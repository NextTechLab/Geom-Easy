using System;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private float reachDistance = 3f;
    [SerializeField] private LayerMask interactionMask;

    private IInteractable interactable;
    private bool isInteracting = false;
    
    private void Update()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);

        if (Input.GetKeyDown(KeyCode.E) &&
            !isInteracting && 
            Physics.Raycast(ray, out RaycastHit hit, reachDistance, interactionMask))
        {
            if (hit.transform.TryGetComponent(out interactable))
            {
                // start interaction
                isInteracting = true;
                interactable.OnInteractionBegin();
            }
        }

        if (isInteracting)
        {
            interactable.OnInteractionTick();
        }
    }
}

public interface IInteractable
{
    void OnInteractionBegin();
    void OnInteractionTick();
    void OnInteractionEnd();
}