using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HopWhenNotTargeted : MonoBehaviour
{
    [SerializeField] private float hopForce = 30f;
    [SerializeField] private float thresholdAngle = 10f;
    [SerializeField] private float range = 20f;
    
    private Transform cam = null;
    private Rigidbody rb = null;
    private bool shouldHop = false;

    private void Awake()
    {
        cam = Camera.main.transform;
        rb = this.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Hop), 
            Random.Range(0f, 3f), 
            Random.Range(1f, 4f));
    }

    private void Update()
    {
        Vector3 camToShape = this.transform.position - cam.position;
        if (camToShape.sqrMagnitude > range * range)
        {
            return;
        }
        
        shouldHop = Vector3.Angle(cam.forward, camToShape) >= thresholdAngle;
    }

    private void Hop()
    {
        if (!shouldHop) return;
        rb.AddForce(Vector3.up * hopForce);
    }
}
