using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int coins;
    protected SaveObject saveObject;
    private string saveJson;

    private TextMeshProUGUI coinText;

    Dictionary<BuildingType, int> buildingCosts;
    List<Building> buildings;
    GameObject foundation;

    private void Awake()
    {
        saveObject = ScriptableObject.CreateInstance("SaveObject") as SaveObject;
        buildings = new List<Building>();
        foundation = (GameObject)Resources.Load("Foundation");
        SetUpLedger();
        coinText = GetComponentInChildren<TextMeshProUGUI>();
        Load();
    }

    private void Start()
    {
        // BuildTown();
    }

    private void FixedUpdate()
    {
        coinText.text = "Coins: " + coins;
    }

    private void SetUpLedger()
    {
        buildingCosts = new Dictionary<BuildingType, int>();
        buildingCosts.Add(BuildingType.House, 100);
        buildingCosts.Add(BuildingType.Church, 1500);
        buildingCosts.Add(BuildingType.Townhall, 800);
        buildingCosts.Add(BuildingType.Windmill, 300);
        buildingCosts.Add(BuildingType.Default, 500);
    }

    /// <summary>
    /// Save the current XP and town layout
    /// </summary>
    public void Save()
    {

        //saveObject = new SaveObject(buildings);
        //saveObject = ScriptableObject.CreateInstance("SaveObject") as SaveObject;
        //saveObject.buildings = buildings;
        saveJson = saveObject.SaveToJson(buildings);
        Debug.Log(saveJson);
        //object toSave = saveObject;
        //saveJson = JsonUtility.ToJson(toSave);
        PlayerPrefs.SetString("town", saveJson);

        PlayerPrefs.SetInt("totalCoins", coins);
    }

    /// <summary>
    /// load previously saved XP and town layout, if none exists yet create object for town layout
    /// </summary>
    private void Load()
    {
        buildings = new List<Building>();

        saveJson = PlayerPrefs.GetString("town");
        if (saveJson == "{}" || saveJson == null)
        {
            saveObject = ScriptableObject.CreateInstance("SaveObject") as SaveObject;
        }
        else
        {
            JsonUtility.FromJsonOverwrite(saveJson, saveObject);
            if (saveObject.buildings == null || saveObject.buildings.Count == 0)
            {
                coins = PlayerPrefs.GetInt("totalCoins", 0);
                return;
            }

            foreach (KeyValuePair<Vector3, BuildingType> kvp in saveObject.buildings)
            {
                BuildBuilding(kvp.Value, kvp.Key);
                //buildings.Add(JsonUtility.FromJson<Building>(s));
            }
        }

        coins = PlayerPrefs.GetInt("totalCoins", 0);
    }

    /// <summary>
    /// build the town that was loaded
    /// </summary>
    private void BuildTown()
    {
        if (buildings.Count > 0)
        {
            foreach (Building b in buildings)
            {
                b.Build(b.GetCoordinate());
            }
        }
    }

    public void CreateBuilding(int _type)
    {
        Mathf.Clamp(_type, 0, 4);
        int cost = buildingCosts[(BuildingType)_type];
        if (coins >= cost)
        {
            coins -= cost;
            BuildBuilding((BuildingType)_type, new Vector3(Random.Range(0, 10), Random.Range(0, 2), Random.Range(0, 10)));
            //IBuilding newBuilding = new Building(new Vector3(Random.Range(0,10), Random.Range(0, 2), Random.Range(0, 10))) as IBuilding;
            /*
            GameObject createdBuilding = Instantiate(foundation);
            createdBuilding.AddComponent<Building>();
            Building newBuilding = createdBuilding.GetComponent<Building>();
            newBuilding.Build(new Vector3((int)Random.Range(0, 10), (int)Random.Range(0, 2.9f), (int)Random.Range(0, 10)));
            Debug.Log(newBuilding);
            buildings.Add(newBuilding);
            */
        } else
        {
            Debug.Log("Not enough coins");
        }
    }

    private void BuildBuilding(BuildingType _type, Vector3 coord)
    {
        GameObject createdBuilding;
        Building newBuilding;
        switch (_type)
        {
            case (BuildingType.House):
                createdBuilding = Instantiate(foundation);
                createdBuilding.AddComponent<House>();
                newBuilding = createdBuilding.GetComponent<Building>();
                newBuilding.Build(coord);
                buildings.Add(newBuilding);
                break;
            case (BuildingType.Church):
                createdBuilding = Instantiate(foundation);
                createdBuilding.AddComponent<Church>();
                newBuilding = createdBuilding.GetComponent<Building>();
                newBuilding.Build(coord);
                buildings.Add(newBuilding);
                break;
            case (BuildingType.Townhall):
                createdBuilding = Instantiate(foundation);
                createdBuilding.AddComponent<Townhall>();
                newBuilding = createdBuilding.GetComponent<Building>();
                newBuilding.Build(coord);
                buildings.Add(newBuilding);
                break;
            case (BuildingType.Windmill):
                createdBuilding = Instantiate(foundation);
                createdBuilding.AddComponent<Windmill>();
                newBuilding = createdBuilding.GetComponent<Building>();
                newBuilding.Build(coord);
                buildings.Add(newBuilding);
                break;
            case (BuildingType.Default):
                createdBuilding = Instantiate(foundation);
                createdBuilding.AddComponent<Building>();
                newBuilding = createdBuilding.GetComponent<Building>();
                newBuilding.Build(coord);
                buildings.Add(newBuilding);
                break;
            default:
                createdBuilding = Instantiate(foundation);
                createdBuilding.AddComponent<Building>();
                newBuilding = createdBuilding.GetComponent<Building>();
                newBuilding.Build(coord);
                buildings.Add(newBuilding);
                break;
        }
    }

    public void StartSurvey()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Add a building to the list of buildings
    /// </summary>
    /// <param name="building"></param>
    private void AddBuilding(Building building)
    {
        buildings.Add(building);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public int GetCoins()
    {
        return coins;
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public enum BuildingType 
    {
        Default,
        House,
        Church,
        Townhall,
        Windmill
    }
}
