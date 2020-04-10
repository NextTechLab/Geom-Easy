using System;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    [SerializeField] private List<Shapes> requiredShapes = new List<Shapes>();
    
    private Stack<Shapes> currentShapes = new Stack<Shapes>();
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shape"))
        {
            Shapes shapeType = other.GetComponent<Shape>().ShapeType;
            if (shapeType == NextRequiredShape())
            {
                currentShapes.Push(shapeType);
                Debug.Log("fits!");
            }
            else
            {
                Debug.Log("no fits!");
            }
            
        }
    }

    private Shapes NextRequiredShape()
    {
        return Shapes.Cube;
    }
}