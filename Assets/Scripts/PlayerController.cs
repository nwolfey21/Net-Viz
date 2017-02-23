using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float step;
	public float rotateSpeed;
	public Mesh objectToCreate;

	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		var gameObject = new GameObject("Sphere");
		var meshFilter = gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();
		meshFilter.sharedMesh = objectToCreate;
		gameObject.transform.position = new Vector3 (1.0f, 1.0f, 1.0f);
		//		gameObject.transform.rotation = transform.rotation;
	}

	void Update()
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
		Vector3 position = transform.position;

//		rb.AddForce (movement * speed);
		rb.MovePosition (position + movement * step);
		Quaternion deltaRotation = Quaternion.Euler(0.0f, rotateHorizontal * rotateSpeed * Time.deltaTime, 0.0f);
//		print ("deltaRotation:" + deltaRotation);
		rb.MoveRotation(rb.rotation * deltaRotation);
		transform.Rotate(rotateVertical*rotateSpeed*Time.deltaTime, 0.0f, 0.0f);
		transform.Rotate(0.0f, rotateHorizontal*rotateSpeed*Time.deltaTime, 0.0f);
	}
}