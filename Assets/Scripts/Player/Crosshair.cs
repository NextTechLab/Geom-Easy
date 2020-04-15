using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private GameObject crosshair = null;

    public bool IsDisplayed
    {
        get => crosshair.activeInHierarchy;
        set
        {
            if (value == crosshair.activeInHierarchy)
            {
                return;
            }
            crosshair.SetActive(value);
        }
    }
}
