using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject : ScriptableObject
{
    //public List<Building> buildings = new List<Building>();
    public List<string> buildingJsons = new List<string>();

    /*public SaveObject(List<Building> buildingList)
    {
        buildings = buildingList;
    }*/

    public string SaveToJson(List<Building> buildings)
    {
        buildingJsons = new List<string>();
        foreach (Building b in buildings)
        {
            buildingJsons.Add(JsonUtility.ToJson(b));
        }
        return JsonUtility.ToJson(this);
    }
}
