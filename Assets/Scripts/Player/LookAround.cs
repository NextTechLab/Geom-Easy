using UnityEngine;

public class LookAround : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 10f;

    private float yLook = 0f;
    private float mouseX = 0f;
    private float mouseY = 0f;
    private Transform cam = null;

    private void Awake()
    {
        cam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        mouseX =  Time.deltaTime * mouseSensitivity * Input.GetAxis("Mouse X");
        mouseY =  Time.deltaTime * mouseSensitivity * Input.GetAxis("Mouse Y");
        yLook -= mouseY;
        cam.localRotation = Quaternion.Euler(
            Mathf.Clamp(yLook, -90f, 90f), 0f, 0f);
        this.transform.Rotate(mouseX * Vector3.up);
    }
}
