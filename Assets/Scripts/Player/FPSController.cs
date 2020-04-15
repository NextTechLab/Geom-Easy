using UnityEngine;

public class FPSController : MonoBehaviour
{
	public float mouseSensitivityX = 1.0f;
	public float mouseSensitivityY = 1.0f;

	public float walkSpeed = 10.0f;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;

	Transform cameraT;
	float verticalLookRotation;

	Rigidbody rigidbodyR;

	float jumpForce = 250.0f;
	bool grounded;
	public LayerMask groundedMask;

	private void Start()
	{
		cameraT = Camera.main.transform;
		rigidbodyR = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		// rotation
		transform.Rotate(Vector3.up * (Input.GetAxis("Mouse X") * mouseSensitivityX));
		verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
		cameraT.localEulerAngles = Vector3.left * verticalLookRotation;

		// movement
		Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
		Vector3 targetMoveAmount = moveDir * walkSpeed;
		moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

		// jump
		if (Input.GetButtonDown("Jump"))
		{
			if (grounded)
			{
				rigidbodyR.AddForce (transform.up * jumpForce);
			}
		}

		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit hit;

		grounded = Physics.Raycast(ray, out hit, 1 + .1f, groundedMask);

	}

	private void FixedUpdate()
	{
		rigidbodyR.MovePosition(
			rigidbodyR.position + transform.TransformDirection (moveAmount) * Time.fixedDeltaTime);
	}
}