using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject : ScriptableObject
{
    public Dictionary<Vector3, GameManager.BuildingType> buildings = new Dictionary<Vector3, GameManager.BuildingType>();
    //public List<string> buildingJsons = new List<string>();

    /*public SaveObject(List<Building> buildingList)
    {
        buildings = buildingList;
    }*/

    public string SaveToJson(List<Building> _buildings)
    {
        /*
        buildingJsons = new List<string>();
        foreach (Building b in _buildings)
        {
            buildingJsons.Add(JsonUtility.ToJson(b));
        }
        */
        foreach (Building b in _buildings)
        {
            buildings.Add(b.GetCoordinate(), b.GetBuildType());
        }

        return JsonUtility.ToJson(this);
    }
}
