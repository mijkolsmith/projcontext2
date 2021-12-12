using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject : ScriptableObject
{
    //public Dictionary<Vector3, GameManager.BuildingType> buildings = new Dictionary<Vector3, GameManager.BuildingType>();
    //public Dictionary<string, string> test = new Dictionary<string, string>();
    //public List<string> buildingJsons = new List<string>();
    public List<BuildingStruct> buildings = new List<BuildingStruct>();

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
        buildings = new List<BuildingStruct>();
        foreach (Building b in _buildings)
        {
            //buildings.Add(b.GetCoordinate(), b.GetBuildType());
            BuildingStruct buildingObj = new BuildingStruct();
            buildingObj.coordinates = b.GetCoordinate();
            buildingObj.buildType = b.GetBuildType();
            buildings.Add(buildingObj);
        }

        return JsonUtility.ToJson(this);
    }
}
