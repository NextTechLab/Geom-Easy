using UnityEngine;
using UnityEngine.Events;

public class StackTracker : MonoBehaviour
{
    [HideInInspector] public UnityEvent statusAlteredEvent = new UnityEvent();
    public bool IsActive
    {
        get => isActive;
        set
        {
            if (value != isActive)
            {
                isActive = value;
                statusAlteredEvent.Invoke();
            }
        }
    } 
    private bool isActive = false;
}
