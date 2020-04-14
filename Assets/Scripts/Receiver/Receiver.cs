using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Shapes> requiredShapes = new List<Shapes>();
    [SerializeField] private ShapeDirectory shapeDirectory = null;
    
    private Stack<Shapes> remainingShapesRequired = null;
    private Stack<Shapes> currentShapes = new Stack<Shapes>();
    private StackTracker tracker = null;

    private void Awake()
    {
        remainingShapesRequired = new Stack<Shapes>(requiredShapes);
        tracker = this.GetComponent<StackTracker>();
        tracker.IsActive = false;
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
                if (remainingShapesRequired.Count <= 0)
                {
                    tracker.IsActive = true;
                }
                Destroy(other.gameObject);
                Debug.Log("fits!");
            }
            else
            {
                Debug.Log("no fits!");
            }
        }
    }

    #region Interactions

    public Transform InteractionHandler { get; set; }
    public void OnInteractionBegin(Interact interactor)
    {
        
        if (currentShapes.Count <= 0)
        {
            return;
        }

        tracker.IsActive = false;
        Shapes toSpawn = currentShapes.Pop();
        remainingShapesRequired.Push(toSpawn);
        Instantiate(shapeDirectory.GetShapePrefab(toSpawn),
            this.transform.position,
            Quaternion.identity);
    }

    public void OnInteractionTick(Interact interactor)
    {
        interactor.EndInteraction();
    }

    public void OnInteractionEnd(Interact interactor)
    {
        Debug.Log("Interaction with pedestal done!");
    }

    #endregion

}