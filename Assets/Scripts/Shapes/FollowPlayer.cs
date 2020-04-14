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
                StartCoroutine(nameof(MoveToPlayer));
            }
        }
    }

    private IEnumerator MoveToPlayer()
    {
        while (shouldFollowPlayer)
        {
            // offset upwards so it doesn't drag along at smaller distances
            Vector3 dir = player.position + (1.5f * Vector3.up) - this.transform.position;
            if (dir.sqrMagnitude >= stoppingDistance * stoppingDistance)
            {
                dir.Normalize();
                rb.AddForce(moveForce * dir);
            }
            yield return new WaitForSeconds(timeBetweenForces);
        }

        yield return null;
    }
}
