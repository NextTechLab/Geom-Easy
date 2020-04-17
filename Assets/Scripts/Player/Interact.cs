using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private float reachDistance = 3f;
    [SerializeField] private LayerMask interactionMask = 1 << 9;

    private IInteractable interactable = null;
    private bool isInteracting = false;
    private Transform cam = null;
    private Crosshair crosshair = null;

    private void Awake()
    {
        cam = Camera.main.transform;
        crosshair = cam.GetComponent<Crosshair>();
    }

    private void Update()
    {
        bool isKeyPressed = Input.GetKeyDown(KeyCode.E);
        if (isInteracting && isKeyPressed)
        {
            EndInteraction();
            return;
        }
        Ray ray = new Ray(cam.position, cam.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, reachDistance, interactionMask))
        {
            if (hit.transform.TryGetComponent(out interactable))            
            {
                crosshair.IsDisplayed = true;
                if (isKeyPressed)
                {
                    BeginInteraction();
                }
            }
        }
        else
        {
            crosshair.IsDisplayed = false;
        }

        if (isInteracting)
        {
            if (interactable == null)
            {
                EndInteraction();
                return;
            }
            interactable.OnInteractionTick(this);
        }
    }

    private void BeginInteraction()
    {
        isInteracting = true;
        interactable.InteractionHandler = cam;
        interactable?.OnInteractionBegin(this);
    }

    public void EndInteraction()
    {
        interactable?.OnInteractionEnd(this);
        isInteracting = false;
        interactable = null;
    }
}