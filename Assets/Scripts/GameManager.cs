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

    List<Building> buildings;

    private void Awake()
    {
        saveObject = ScriptableObject.CreateInstance("SaveObject") as SaveObject;
        buildings = new List<Building>();
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

    /// <summary>
    /// Save the current XP and town layout
    /// </summary>
    public void Save()
    {
        /*
        //saveObject = new SaveObject(buildings);
        //saveObject = ScriptableObject.CreateInstance("SaveObject") as SaveObject;
        //saveObject.buildings = buildings;
        saveJson = saveObject.SaveToJson(buildings);
        Debug.Log(saveJson);
        //object toSave = saveObject;
        //saveJson = JsonUtility.ToJson(toSave);
        PlayerPrefs.SetString("town", saveJson);
        */
        PlayerPrefs.SetInt("totalCoins", coins);
    }

    /// <summary>
    /// load previously saved XP and town layout, if none exists yet create object for town layout
    /// </summary>
    private void Load()
    {
        /*
        saveJson = PlayerPrefs.GetString("town");
        if (saveJson == "{}" || saveJson == null)
        {
            saveObject = ScriptableObject.CreateInstance("SaveObject") as SaveObject;
            buildings = new List<Building>();
        }
        else
        {
            JsonUtility.FromJsonOverwrite(saveJson, saveObject);
            if (saveObject.buildingJsons == null || saveObject.buildingJsons.Count == 0) 
            {
                coins = PlayerPrefs.GetInt("totalCoins", 0);
                return;
            }
            foreach (string s in saveObject.buildingJsons)
            {
                buildings.Add(JsonUtility.FromJson<Building>(s));
            }
        }
        */
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

    public void CreateBuilding(int cost)
    {
        if (coins >= cost)
        {
            coins -= cost;
            //IBuilding newBuilding = new Building(new Vector3(Random.Range(0,10), Random.Range(0, 2), Random.Range(0, 10))) as IBuilding;
            GameObject createdBuilding = Instantiate(Resources.Load("Foundation")) as GameObject;
            createdBuilding.AddComponent<Building>();
            Building newBuilding = createdBuilding.GetComponent<Building>();
            newBuilding.Build(new Vector3((int)Random.Range(0, 10), (int)Random.Range(0, 2.9f), (int)Random.Range(0, 10)));
            Debug.Log(newBuilding);
            buildings.Add(newBuilding);
        } else
        {
            Debug.Log("Not enough coins");
        }
    }    
    
    public void CreateHouse(int cost)
    {
        if (coins >= cost)
        {
            coins -= cost;
            GameObject createdBuilding = Instantiate(Resources.Load("Foundation")) as GameObject;
            createdBuilding.AddComponent<House>();
            Building newBuilding = createdBuilding.GetComponent<House>();
            newBuilding.Build(new Vector3(Random.Range(0, 10), Random.Range(0, 2), Random.Range(0, 10)));
            //Debug.Log(newBuilding);
            //IBuilding newBuilding = new House(new Vector3(Random.Range(0, 10), Random.Range(0, 2), Random.Range(0, 10))) as IBuilding;
            AddBuilding(newBuilding);
        } else
        {
            Debug.Log("Not enough coins");
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
}
