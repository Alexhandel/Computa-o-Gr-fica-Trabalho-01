using UnityEngine;
using System.Collections;

public class ThirdPersonCharacterController : MonoBehaviour
{

	public float runSpeed;

	public GameObject camera;

	private Rigidbody rb;
	private Animator anim;
	private int
		verticalHash, horizontalHash, magnitudeHash;

	public float
		//rotationSpeed;
		rotationOffset,
		inputDampTime = .25f;

	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		//camera = GameObject.FindGameObjectWithTag("MainCamera");
		horizontalHash = Animator.StringToHash("InputHorizontal");
		verticalHash = Animator.StringToHash("InputVertical");
		magnitudeHash = Animator.StringToHash("InputMagnitude(Squared)");
	}

	// Update is called once per frame
	void Update()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		Vector3 inputDirection = new Vector3(h, 0f, v);
		Vector3 cameraForward = Vector3.ProjectOnPlane(camera.transform.forward, Vector3.up);

		Debug.DrawRay(transform.position + new Vector3(0f, 1f, 0f), cameraForward, Color.blue);

		anim.SetFloat(verticalHash, v, inputDampTime, Time.deltaTime);
		anim.SetFloat(horizontalHash, h, inputDampTime, Time.deltaTime);
		anim.SetFloat(magnitudeHash, new Vector2(h, v).sqrMagnitude);

		if (inputDirection.sqrMagnitude > 0)
		{
			Debug.DrawRay(transform.position + new Vector3(0f, 1f, 0f), inputDirection, Color.green);
			Debug.DrawRay(transform.position + new Vector3(0f, 1f, 0f), inputDirection, Color.magenta);

			Vector3 targetDirection;
			targetDirection = camera.transform.TransformDirection(inputDirection) * 2;
			targetDirection.y = 0.0f;

			Debug.DrawRay(transform.position + new Vector3(0f, 1f, 0f), inputDirection, Color.red);
			Debug.DrawRay(transform.position + new Vector3(0f, 1f, 0f), targetDirection, Color.yellow);

			transform.LookAt(transform.position + targetDirection);

			Vector3 speedVec = (transform.forward * runSpeed);
			rb.velocity = new Vector3(speedVec.x, rb.velocity.y, speedVec.z);
			Debug.Log("Velocity: " + rb.velocity);
		}
	}
}
