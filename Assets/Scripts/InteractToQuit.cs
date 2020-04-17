using UnityEngine;

public class InteractToQuit : MonoBehaviour, IInteractable
{
    public Transform InteractionHandler { get; set; }
    public void OnInteractionBegin(Interact interactor)
    {
        Application.Quit();
    }

    public void OnInteractionTick(Interact interactor)
    {
        
    }

    public void OnInteractionEnd(Interact interactor)
    {
        
    }
}
