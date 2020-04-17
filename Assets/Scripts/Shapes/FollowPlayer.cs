using System;
using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public bool shouldFollowPlayer = false;
    [SerializeField] private float moveForce = 250f;
    [SerializeField] private float timeBetweenForces = 1f;
    [SerializeField] private float stoppingDistance = 10f;
    
    private Transform player = null;
    private Rigidbody rb = null;
    private Vector3 initialPosition = Vector3.zero;
    
    private void Awake()
    {
        player = Camera.main.transform.parent;
        rb = this.GetComponent<Rigidbody>();
        initialPosition = this.transform.position;
        StartCoroutine(nameof(MoveToPlayer));

    }

    private void Update()
    {
        // TODO: remove debug code
        if (!shouldFollowPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                shouldFollowPlayer = true;
            }
        }
    }

    private IEnumerator MoveToPlayer()
    {
        // offset upwards so it doesn't drag along at smaller distances
        Vector3 dir = player.position + (1.5f * Vector3.up) - this.transform.position;
        float sqrMagnitude = dir.sqrMagnitude;
        if (sqrMagnitude > 10000f)
        {
            // sanity check
            this.transform.position = initialPosition;
        }
        while (shouldFollowPlayer)
        {
            
            if (sqrMagnitude >= stoppingDistance * stoppingDistance)
            {
                dir.Normalize();
                rb.AddForce(moveForce * dir);
            }
            yield return new WaitForSeconds(timeBetweenForces);
        }

        yield return null;
    }
}
