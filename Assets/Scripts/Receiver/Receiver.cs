using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StackAlterEvent : UnityEvent<Shapes>
{}

public class Receiver : MonoBehaviour, IInteractable
{
    public List<Shapes> requiredShapes = new List<Shapes>();
    [SerializeField] private ShapeDirectory shapeDirectory = null;

    [HideInInspector] public Stack<Shapes> remainingShapesRequired = null;
    [HideInInspector] public Stack<Shapes> currentShapes = new Stack<Shapes>();
    
    [HideInInspector] public StackAlterEvent addShapeEvent = new StackAlterEvent();
    [HideInInspector] public StackAlterEvent removeShapeEvent = new StackAlterEvent();
    
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
                Shapes addedShape = remainingShapesRequired.Pop();
                currentShapes.Push(addedShape);
                if (remainingShapesRequired.Count <= 0)
                {
                    tracker.IsActive = true;
                }
                Destroy(other.gameObject);
                addShapeEvent.Invoke(addedShape);
            }
            else
            {
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
        Shapes removedShape = currentShapes.Pop();
        remainingShapesRequired.Push(removedShape);
        Instantiate(shapeDirectory.GetShapePrefab(removedShape),
            this.transform.position + 1f * -this.transform.right,
            Quaternion.identity).
            GetComponent<FollowPlayer>().shouldFollowPlayer = true;
        removeShapeEvent.Invoke(removedShape);
    }

    public void OnInteractionTick(Interact interactor)
    {
        interactor.EndInteraction();
    }

    public void OnInteractionEnd(Interact interactor)
    {
        
    }

    #endregion

}