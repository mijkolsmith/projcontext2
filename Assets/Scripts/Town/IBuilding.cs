using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding
{
    public Vector3 GetCoordinate();
    public Vector2 GetSize();
    public void Build(Vector3 pos);
    public GameManager.BuildingType GetBuildType();
}
