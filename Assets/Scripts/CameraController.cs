using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float speed;
	public float step;

	void Start ()
	{

	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float rotateHorizontal = Input.GetAxis ("RotateHorizontal");
		float rotateVertical = Input.GetAxis ("RotateVertical");

//		print ("horizontal:" + moveHorizontal + " vertical:" + moveVertical);
//		print ("rotateHorizontal:" + rotateHorizontal + " rotateVertical:" + rotateVertical);

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		//		rb.AddForce (movement * speed);
		transform.position = transform.position + movement * step;
		transform.Rotate(rotateVertical*speed*Time.deltaTime, 0.0f, 0.0f);
		transform.Rotate(0.0f, rotateHorizontal*speed*Time.deltaTime, 0.0f);
	}
}