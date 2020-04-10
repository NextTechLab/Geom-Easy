using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    [SerializeField] private Shapes shapeType;

    public Shapes ShapeType => shapeType;
}
