using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Struct to save building info to
/// </summary>
[System.Serializable]
    public struct BuildingStruct
{
    public Vector3 coordinates;
    public GameManager.BuildingType buildType;
}
