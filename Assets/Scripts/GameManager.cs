using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int coins;                                      //total amount of coins available
    protected SaveObject saveObject;                        //object to encapsulate list for saving using json
    private string saveJson;                                //the json string for saving town layout

    private TextMeshProUGUI coinText;                       //UI element displaying current amount of coins

    private Dictionary<BuildingType, int> buildingCosts;    //ledger to set coin cost per building
    private List<Building> buildings;                       //list of buildings currently in town
    private GameObject foundation;                          //foundation to copy for new buildings

    /// <summary>
    /// Initiate parameters, Load town & coins
    /// </summary>
    private void Awake()
    {
        saveObject = ScriptableObject.CreateInstance("SaveObject") as SaveObject;
        buildings = new List<Building>();
        foundation = (GameObject)Resources.Load("Foundation");
        SetUpLedger();
        coinText = GetComponentInChildren<TextMeshProUGUI>();
        Load();
    }

    /// <summary>
    /// update UI to reflect current amount of coins
    /// </summary>
    private void FixedUpdate()
    {
        coinText.text = "Coins: " + coins;
    }

    /// <summary>
    /// delete references to current buildings
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteKey("town");
            foreach (Building b in buildings)
            {
                Destroy(b.gameObject, 0);
            }
            buildings = new List<Building>();
        }
    }

    /// <summary>
    /// initiate dictionary for different building costs
    /// </summary>
    private void SetUpLedger()
    {
        buildingCosts = new Dictionary<BuildingType, int>();
        buildingCosts.Add(BuildingType.House, 10);
        buildingCosts.Add(BuildingType.Church, 80);
        buildingCosts.Add(BuildingType.Townhall, 50);
        buildingCosts.Add(BuildingType.Windmill, 20);
        buildingCosts.Add(BuildingType.Default, 50);
    }

    /// <summary>
    /// Save the current coins and town layout to playerprefs
    /// </summary>
    public void Save()
    {
        saveJson = saveObject.SaveToJson(buildings);
        //Debug.Log(saveJson);
        PlayerPrefs.SetString("town", saveJson);

        PlayerPrefs.SetInt("totalCoins", coins);
    }

    /// <summary>
    /// load previously saved coins and town layout, if none exists yet create object for town layout
    /// </summary>
    private void Load()
    {
        buildings = new List<Building>();

        saveJson = PlayerPrefs.GetString("town");
        //if no layout has been saved yet, create a new object to hold the layout
        if (saveJson == "{}" || saveJson == null)
        {
            saveObject = ScriptableObject.CreateInstance("SaveObject") as SaveObject;
        }
        else
        {
            //overwrite saveObject with data saved to playerprefs
            JsonUtility.FromJsonOverwrite(saveJson, saveObject);

            //if no buildings have been saved yet, only get current amount of coins
            if (saveObject.buildings == null || saveObject.buildings.Count == 0)
            {
                coins = PlayerPrefs.GetInt("totalCoins", 0);
                return;
            }

            //build the saved buildings
            foreach (BuildingStruct b in saveObject.buildings)
            {
                BuildBuilding(b.buildType, b.coordinates);
            }
        }

        coins = PlayerPrefs.GetInt("totalCoins", 0);
    }

    /// <summary>
    /// buy and build a new building if the player has enough coins
    /// </summary>
    /// <param name="_type">the int corresponding to the enum BuilingType</param>
    public void CreateBuilding(int _type)
    {
        Mathf.Clamp(_type, 0, 4);
        int cost = buildingCosts[(BuildingType)_type];
        if (coins >= cost)
        {
            coins -= cost;
            BuildBuilding((BuildingType)_type, new Vector3(Random.Range(0, 50), Random.Range(0, 2), Random.Range(0, 50)));
        } else
        {
            Debug.Log("Not enough coins");
        }
    }

    /// <summary>
    /// Build the building and add it to the list of buildings
    /// </summary>
    /// <param name="_type">the building type</param>
    /// <param name="coord">the coordinate to build the building at</param>
    private void BuildBuilding(BuildingType _type, Vector3 coord)
    {
        GameObject createdBuilding;
        Building newBuilding;

        //create a foundation to hold the building
        createdBuilding = Instantiate(foundation);

        //add the right child component per building type to the foundation
        switch (_type)
        {
            case (BuildingType.House):
                createdBuilding.AddComponent<House>();
                break;
            case (BuildingType.Church):
                createdBuilding.AddComponent<Church>();
                break;
            case (BuildingType.Townhall):
                createdBuilding.AddComponent<Townhall>();
                break;
            case (BuildingType.Windmill):
                createdBuilding.AddComponent<Windmill>();
                break;
            case (BuildingType.Default):
                createdBuilding.AddComponent<Building>();
                break;
            default:
                createdBuilding.AddComponent<Building>();
                break;
        }

        //use the added building component to build the correct building and save to buildings list
        newBuilding = createdBuilding.GetComponent<Building>();
        newBuilding.Build(coord);
        buildings.Add(newBuilding);
    }

    /// <summary>
    /// save coins and town and go to the survey scene
    /// </summary>
    public void StartSurvey()
    {
        Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// add coins to the total amount of coins available
    /// </summary>
    /// <param name="amount">the amount of coins to add</param>
    public void AddCoins(int amount)
    {
        coins += amount;
    }

    /// <summary>
    /// get the current amount of posessed coins
    /// </summary>
    /// <returns>total coins posessed</returns>
    public int GetCoins()
    {
        return coins;
    }

    /// <summary>
    /// save coins and town on quit
    /// </summary>
    private void OnApplicationQuit()
    {
        Save();
    }

    /// <summary>
    /// the type of building
    /// </summary>
    [System.Serializable]
    public enum BuildingType 
    {
        Default,
        House,
        Church,
        Townhall,
        Windmill
    }
}
