using System;
using UnityEngine;

public class HopWhenNotTargeted : MonoBehaviour
{
    [SerializeField] private float hopForce = 60f;
    [SerializeField] private float thresholdAngle = 10f;
    [SerializeField] private float range = 20f;
    
    private Transform cam = null;
    private Rigidbody rb = null;

    private void Awake()
    {
        cam = Camera.main.transform;
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 camToShape = this.transform.position - cam.position;
        if (camToShape.sqrMagnitude > range * range)
        {
            return;
        }
        
        if (Vector3.Angle(cam.forward, camToShape) >= thresholdAngle)
        {
            Hop();
            // Debug.Log("you're not looking at me! " + this.name);
        }
    }

    private void Hop()
    {
        Debug.Log("Hop! " + this.name);
    }
}
