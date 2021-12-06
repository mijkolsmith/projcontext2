using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IBuilding
{
    [SerializeReference] protected Vector3 coordinate;
    [SerializeField] protected Vector3 size;

    protected Object model;

    public Building(Vector3 pos)
    {
        Build(pos);
    }
    
    protected virtual void CreateBuilding(Vector3 pos)
    {
        model = Resources.Load("Building");
        coordinate = pos;
        size = new Vector3(3, 1, 2);
    }

    public Vector3 GetCoordinate()
    {
        return coordinate;
    }

    public Vector2 GetSize()
    {
        return size;
    }

    public virtual void Build(Vector3 pos)
    {
        CreateBuilding(pos);
        GameObject structure = Instantiate(model, this.transform) as GameObject;
        structure.transform.position = coordinate;
        structure.transform.localScale = Vector3.Scale(structure.transform.localScale, size);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
