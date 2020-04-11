using UnityEngine;

public class Shape : MonoBehaviour, IInteractable
{
    public Shapes ShapeType => shapeType;
    public Transform InteractionHandler { get; set; }
    
    [SerializeField] private Shapes shapeType = Shapes.Null;
    [SerializeField] private float carryOffset = 3f;
    [SerializeField][Range(0f, 60f)] private float snapSpeed = 40f;

    private Rigidbody rb = null;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    public void OnInteractionBegin()
    {
        rb.isKinematic = true;
    }

    public void OnInteractionTick()
    {
        this.transform.position = Vector3.Lerp(
            this.transform.position,
            InteractionHandler.position + InteractionHandler.forward * carryOffset,
            Time.deltaTime * snapSpeed);
    }

    public void OnInteractionEnd()
    {
        rb.isKinematic = false;
    }
}
