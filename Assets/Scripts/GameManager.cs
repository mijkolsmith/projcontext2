using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int coins;
    private SaveObject saveObject;
    private string saveJson;

    List<IBuilding> buildings;

    private void Awake()
    {
        saveObject = ScriptableObject.CreateInstance("SaveObject") as SaveObject;
        buildings = new List<IBuilding>();
        Load();
        BuildTown();
    }

    private void FixedUpdate()
    {
        
    }

    /// <summary>
    /// Save the current XP and town layout
    /// </summary>
    public void Save()
    {
        //saveObject = new SaveObject(buildings);
        //saveObject = ScriptableObject.CreateInstance("SaveObject") as SaveObject;
        saveObject.buildings = buildings;
        saveJson = JsonUtility.ToJson(saveObject);
        PlayerPrefs.SetString("town", saveJson);
        PlayerPrefs.SetInt("totalCoins", coins);
    }

    /// <summary>
    /// load previously saved XP and town layout, if none exists yet create object for town layout
    /// </summary>
    private void Load()
    {
        saveJson = PlayerPrefs.GetString("town");
        if (saveJson == "")
        {
            saveObject = ScriptableObject.CreateInstance("SaveObject") as SaveObject;
            buildings = new List<IBuilding>();
        }
        else
        {
            JsonUtility.FromJsonOverwrite(saveJson, saveObject);
            buildings = saveObject.buildings;
        }

        coins = PlayerPrefs.GetInt("totalCoins", 0);
    }

    /// <summary>
    /// build the town that was loaded
    /// </summary>
    private void BuildTown()
    {
        if (buildings != null)
        {
            foreach (IBuilding b in buildings)
            {
                b.Build(new Vector3(Random.Range(0, 10), Random.Range(0, 2), Random.Range(0, 10)));
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
            IBuilding newBuilding = createdBuilding.GetComponent<Building>();
            newBuilding.Build(new Vector3(Random.Range(0, 10), Random.Range(0, 2), Random.Range(0, 10)));
            Debug.Log(newBuilding);
            //buildings.Add(newBuilding);
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
            IBuilding newBuilding = createdBuilding.GetComponent<House>();
            newBuilding.Build(new Vector3(Random.Range(0, 10), Random.Range(0, 2), Random.Range(0, 10)));
            Debug.Log(newBuilding);
            //IBuilding newBuilding = new House(new Vector3(Random.Range(0, 10), Random.Range(0, 2), Random.Range(0, 10))) as IBuilding;
            //AddBuilding(newBuilding);
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
    private void AddBuilding(IBuilding building)
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
