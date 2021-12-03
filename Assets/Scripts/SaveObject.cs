using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject : ScriptableObject
{
    public List<IBuilding> buildings = new List<IBuilding>();

    public SaveObject(List<IBuilding> buildingList)
    {
        buildings = buildingList;
    }
}
