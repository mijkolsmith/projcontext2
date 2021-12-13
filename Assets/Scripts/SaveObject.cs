using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject : ScriptableObject
{
    //list of BuildingStructs containing info on each saved building
    public List<BuildingStruct> buildings = new List<BuildingStruct>();

    /// <summary>
    /// create a json string cotaining a list of BuildingStructs to save to playerprefs
    /// </summary>
    /// <param name="_buildings">list of buildings currently in town</param>
    /// <returns>json with list of BuildingStructs</returns>
    public string SaveToJson(List<Building> _buildings)
    {
        //initialize new list to avoid saving double
        buildings = new List<BuildingStruct>();
        
        //create struct for each building in town and save those to BuildingStruct list
        foreach (Building b in _buildings)
        {
            BuildingStruct buildingObj = new BuildingStruct
            {
                coordinates = b.GetCoordinate(),
                buildType = b.GetBuildType()
            };
            buildings.Add(buildingObj);
        }

        //return json string containing list of BuildingStructs
        return JsonUtility.ToJson(this);
    }
}
