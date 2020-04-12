using System;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private float reachDistance = 3f;
    [SerializeField] private LayerMask interactionMask = 1 << 9;

    private IInteractable interactable = null;
    private bool isInteracting = false;
    private Transform cam = null;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (isInteracting)
            {
                EndInteraction();
                return;
            }
            
            Ray ray = new Ray(cam.position, cam.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, reachDistance, interactionMask))
            {
                if (hit.transform.TryGetComponent(out interactable))
                {
                    BeginInteraction();
                }
            }
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