using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building
{
    public House(Vector3 coordinate) : base(coordinate)
    {
    }

    protected override void CreateBuilding(Vector3 pos)
    {
        model = Resources.Load("House");
        coordinate = pos;
        size = new Vector3(1, 1, 2);
    }

    public override void Build(Vector3 pos)
    {
        base.Build(pos);
    }
}
