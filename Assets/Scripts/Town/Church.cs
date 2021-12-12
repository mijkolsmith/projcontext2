using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Church : Building
{
    public Church(Vector3 coordinate) : base(coordinate)
    {
    }

    protected override void CreateBuilding(Vector3 pos)
    {
        model = Resources.Load("Church");
        if (model == null) model = Resources.Load("Building");
        coordinate = pos;
        size = new Vector3(2, 2, 4);
    }

    public override void Build(Vector3 pos)
    {
        base.Build(pos);
    }

    public override GameManager.BuildingType GetBuildType()
    {
        return GameManager.BuildingType.Church;
    }
}
