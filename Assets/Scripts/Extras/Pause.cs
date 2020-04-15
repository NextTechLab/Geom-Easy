using System;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private bool canPause = true;
    [SerializeField] private GameObject pausePanel = null;

    private bool isPaused = false;

    private void Update()
    {
        if (!canPause) return;
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pausePanel.SetActive(isPaused.Toggle());
            
            if (!Cursor.visible)
            {
                UnlockMouse();
            }
            else
            {
                LockMouse();
            }
        }

        Time.timeScale = isPaused ? 0f : 1f;
    }
    
    private void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
}
