using System;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public bool shouldFollowPlayer = false;
    [SerializeField] private float moveForce = 250f;
    [SerializeField] private float timeBetweenForces = 1f;
    [SerializeField] private float stoppingDistance = 10f;
    
    private Transform player = null;
    private Rigidbody rb = null;
    private void Awake()
    {
        player = Camera.main.transform.parent;
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // debug code
        if (!shouldFollowPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                shouldFollowPlayer = true;
                InvokeRepeating(nameof(MoveToPlayer), 0f, timeBetweenForces);
            }
        }
    }

    private void MoveToPlayer()
    {
        Vector3 dir = player.position + (1.5f * Vector3.up) - this.transform.position;
        if (dir.sqrMagnitude <= stoppingDistance * stoppingDistance) return;
        
        dir.Normalize();
        rb.AddForce(moveForce * dir);
    }
}
