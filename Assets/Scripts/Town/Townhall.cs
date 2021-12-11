using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Townhall : Building
{
    public Townhall(Vector3 coordinate) : base(coordinate)
    {

    }

    protected override void CreateBuilding(Vector3 pos)
    {
        model = Resources.Load("Townhall");
        if (model == null) model = Resources.Load("Building");
        coordinate = pos;
        size = new Vector3(2, 2, 3);
    }

    public override void Build(Vector3 pos)
    {
        base.Build(pos);
    }

    public override GameManager.BuildingType GetBuildType()
    {
        return GameManager.BuildingType.Townhall;
    }
}
