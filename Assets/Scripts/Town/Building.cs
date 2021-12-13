using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Building : MonoBehaviour, IBuilding
{
    [SerializeReference] protected Vector3 coordinate;  //coordinate the building is set at
    [SerializeField] protected Vector3 size;            //the size of the building

    protected Object model;                             //the model to display for the building
    protected GameObject structure;                     //gameobject of the physical building

    public Building(Vector3 pos)
    {
        Build(pos);
    }

    /// <summary>
    /// get the position of the building
    /// </summary>
    /// <returns>position of building</returns>
    public Vector3 GetCoordinate()
    {
        return coordinate;
    }

    /// <summary>
    /// get the size of the building
    /// </summary>
    /// <returns>building size</returns>
    public Vector2 GetSize()
    {
        return size;
    }

    /// <summary>
    /// build the building
    /// </summary>
    /// <param name="pos">position to build at</param>
    public virtual void Build(Vector3 pos)
    {
        CreateBuilding(pos);
        structure = Instantiate(model, this.transform) as GameObject;
        structure.transform.position = coordinate;
        //structure.transform.localScale = Vector3.Scale(structure.transform.localScale, size);
    }
    
    /// <summary>
    /// create a building and set the parameters to the correct ones for this building type
    /// </summary>
    /// <param name="pos">position to build building at</param>
    protected virtual void CreateBuilding(Vector3 pos)
    {
        model = Resources.Load("Building");
        coordinate = pos;
        size = new Vector3(3, 1, 2);
    }

    /// <summary>
    /// get the specific type of building this is
    /// </summary>
    /// <returns>the actual building type</returns>
    public virtual GameManager.BuildingType GetBuildType()
    {
        return GameManager.BuildingType.Default;
    }
}
