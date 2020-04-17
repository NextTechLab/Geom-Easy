using System;
using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public bool shouldFollowPlayer = false;
    [SerializeField] private float moveForce = 200f;
    [SerializeField] private float timeBetweenForces = 0.5f;
    [SerializeField] private float stoppingDistance = 3f;
    
    private Transform player = null;
    private Rigidbody rb = null;
    private Vector3 initialPosition = Vector3.zero;

    private float sqrMagnitude = 0f;
    
    private void Awake()
    {
        player = Camera.main.transform.parent;
        rb = this.GetComponent<Rigidbody>();
        initialPosition = this.transform.position;
    }

    private void Start()
    {
        StartCoroutine(nameof(MoveToPlayer));
    }

    private IEnumerator MoveToPlayer()
    {
        while (true)
        {
            Vector3 dir = player.position + (1.5f * Vector3.up) - this.transform.position;
            sqrMagnitude = dir.sqrMagnitude;

            if (!shouldFollowPlayer && sqrMagnitude < 16f)
            {
                shouldFollowPlayer = true;
            }
            if (sqrMagnitude > 100000f)
            {
                // sanity check
                this.transform.position = initialPosition;
            }
            if (shouldFollowPlayer && sqrMagnitude >= stoppingDistance * stoppingDistance)
            {
                dir.Normalize();
                rb.AddForce(moveForce * dir);
            }
            yield return new WaitForSeconds(timeBetweenForces);
        }
        yield return new WaitForSeconds(timeBetweenForces);
    }
}
