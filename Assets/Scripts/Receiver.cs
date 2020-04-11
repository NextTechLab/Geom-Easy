using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    [SerializeField] private List<Shapes> requiredShapes = new List<Shapes>();

    private Stack<Shapes> remainingShapesRequired = null;
    private Stack<Shapes> currentShapes = new Stack<Shapes>();

    private void Awake()
    {
        remainingShapesRequired = new Stack<Shapes>(requiredShapes);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (remainingShapesRequired.Count <= 0)
        {
            return;
        }
        
        if (other.CompareTag("Shape"))
        {
            Shapes shapeType = other.GetComponent<Shape>().ShapeType;
            if (shapeType == remainingShapesRequired.Peek())
            {
                currentShapes.Push(remainingShapesRequired.Pop());
                Debug.Log("fits!");
            }
            else
            {
                Debug.Log("no fits!");
            }
            
        }
    }
}