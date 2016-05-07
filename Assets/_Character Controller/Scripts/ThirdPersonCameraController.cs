using UnityEngine;
using System.Collections;

public class ThirdPersonCameraController : MonoBehaviour
{

	[SerializeField]	private float turnSpeed = 4.0f;
	[SerializeField]	private Transform player;
	[SerializeField]	private Transform cameraOffset;
	

	[SerializeField]	private	float maxAngleY;
	[SerializeField]	private float minAngleY;

	private Vector2 angle;
	private Vector3 offset;	
	private Vector3 baseOffset;

	void Start()
	{
		transform.position = cameraOffset.position;
		baseOffset = cameraOffset.position - player.position;
		angle.x = 0;
		angle.y = 10;
		Cursor.lockState = CursorLockMode.Locked;
    }

	void LateUpdate()
	{
		// Horizontal
		angle.x += Input.GetAxis("Mouse X") * turnSpeed;
		angle.y %= 360;

		// Vertical

		angle.y += Input.GetAxis("Mouse Y") * turnSpeed;
		if (angle.y < minAngleY)	angle.y = minAngleY;
		if (angle.y > maxAngleY)	angle.y = maxAngleY;

		offset = baseOffset;		
		offset = Quaternion.AngleAxis(angle.y, Vector3.left) * offset;
		offset = Quaternion.AngleAxis(angle.x, Vector3.up) * offset;		

		//Debug.Log("Angle:" + angle);
		//Debug.Log("Offset:" + offset);

		transform.position = offset + player.position;				
		transform.LookAt(player.position + Vector3.up);
	}
}