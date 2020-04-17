using System;
using UnityEngine;

public class Shape : MonoBehaviour, IInteractable
{
    public Shapes ShapeType => shapeType;
    [SerializeField] private Shapes shapeType = Shapes.Null;
    
    [SerializeField] private float carryOffset = 3f;
    [SerializeField][Range(0f, 60f)] private float snapSpeed = 40f;

    private Rigidbody rb = null;
    private Collider boxCollider = null;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        boxCollider = this.GetComponent<Collider>();
        
    }

    #region Interactions

    public Transform InteractionHandler { get; set; }
    public void OnInteractionBegin(Interact interactor)
    {
        rb.isKinematic = true;
        boxCollider.enabled = false;
    }

    public void OnInteractionTick(Interact interactor)
    {
        rb.MovePosition(Vector3.Lerp(
            this.transform.position,
            InteractionHandler.position + InteractionHandler.forward * carryOffset,
            Time.deltaTime * snapSpeed));
    }

    public void OnInteractionEnd(Interact interactor)
    {
        Ray ray = new Ray(InteractionHandler.position, InteractionHandler.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, carryOffset, 1 << 8))
        {
            rb.MovePosition(hit.point + 0.5f * Vector3.up);
        }
        rb.isKinematic = false;
        boxCollider.enabled = true;
    }
    
    #endregion
}
