using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float gravity = 2f;
    [SerializeField] private LayerMask whatIsGround = 1 << 8;
    
    private CharacterController controller = null;
    private bool isGrounded = true;
    private Vector3 velocity = Vector3.zero;
    private float xInput = 0f;
    private float zInput = 0f;
    
    private void Awake()
    {
        controller = this.GetComponent<CharacterController>();
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(this.transform.position, 0.4f, whatIsGround);

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -1f;
        }

        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
        Vector3 movementDelta = zInput * this.transform.forward +
                                xInput * this.transform.right;
        movementDelta *= Time.deltaTime * movementSpeed;
        
        velocity.y -= Time.deltaTime * gravity;
        
        controller.Move(movementDelta + velocity);

    }
}
