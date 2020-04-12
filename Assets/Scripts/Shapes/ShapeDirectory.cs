using System;
using System.Collections.Generic;
using UnityEngine;

public enum Shapes
{
    Cube = 0,
    Cuboid,
    Parallelopiped,
    Diamond,
    Null
}

[System.Serializable]
public class ShapeData
{
    public Shapes ID;
    public GameObject prefab;
}

[CreateAssetMenu(menuName = "Shape Directory")]
public class ShapeDirectory : ScriptableObject
{
    [SerializeField] private List<ShapeData> directory = null;

    public GameObject GetShapePrefab(Shapes ID)
    {
        foreach (ShapeData data in directory)
        {
            if (ID == data.ID)
            {
                return data.prefab;
            }
        }
        throw new Exception("Directory doesn't have an entry for this ID.");
    }
}