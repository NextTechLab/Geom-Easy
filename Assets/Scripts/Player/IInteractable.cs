using UnityEngine;

public interface IInteractable
{
    Transform InteractionHandler { get; set; }

    void OnInteractionBegin();
    void OnInteractionTick();
    void OnInteractionEnd();
}