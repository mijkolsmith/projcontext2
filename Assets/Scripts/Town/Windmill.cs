using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : Building
{
    public Windmill(Vector3 coordinate) : base(coordinate)
    {

    }

    protected override void CreateBuilding(Vector3 pos)
{
    model = Resources.Load("Windmill");
    if (model == null) model = Resources.Load("Building");
    coordinate = pos;
    size = new Vector3(1, 3, 2);
}

public override void Build(Vector3 pos)
{
    base.Build(pos);
}

public override GameManager.BuildingType GetBuildType()
{
    return GameManager.BuildingType.Windmill;
}
}
