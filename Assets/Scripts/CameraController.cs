using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float speed;
	public float step;
	public float elevationSpeed;

	public float lookSpeed = 4.0F;
	public float tiltAngle = 30.0F;

	private float elevation;

	void Start ()
	{
		elevation = 0.0f;
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float rotateHorizontal = Input.GetAxis ("RotateHorizontal");
		float rotateVertical = Input.GetAxis ("RotateVertical");

		if (Input.GetButton ("Elevate"))
			elevation = elevationSpeed;
		if (Input.GetButton ("Sink"))
			elevation = (-1.0f) * elevationSpeed;

//		print ("elevation:" + elevation + " ");

//		print ("horizontal:" + moveHorizontal + " vertical:" + moveVertical);
//		print ("rotateHorizontal:" + rotateHorizontal + " rotateVertical:" + rotateVertical);

		Vector3 movement = new Vector3 (moveHorizontal, elevation, moveVertical);

		//		rb.AddForce (movement * speed);
		transform.position = transform.position + movement * step;
//		transform.Rotate(rotateVertical*speed*Time.deltaTime, 0.0f, 0.0f);
//		transform.Rotate(0.0f, rotateHorizontal*speed*Time.deltaTime, 0.0f);

		float tiltAroundZ = rotateHorizontal * tiltAngle;
		float tiltAroundX = rotateVertical * tiltAngle;
		Quaternion target = Quaternion.Euler(tiltAroundX, tiltAroundZ, 0);
		transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * lookSpeed);

		elevation = 0.0f;
	}
}