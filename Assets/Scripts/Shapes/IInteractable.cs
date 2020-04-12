using UnityEngine;

public interface IInteractable
{
    Transform InteractionHandler { get; set; }

    void OnInteractionBegin(Interact interactor);
    void OnInteractionTick(Interact interactor);
    void OnInteractionEnd(Interact interactor);
}