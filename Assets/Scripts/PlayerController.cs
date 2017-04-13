using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float step;
	public float rotateSpeed;
	public float elevationSpeed;
	public Mesh objectToCreate;

	private Rigidbody rb;
	private float elevation;

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
		// Sideways movement input
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// Forward/Backward movement input
		float rotateHorizontal = Input.GetAxis ("RotateHorizontal");
		float rotateVertical = Input.GetAxis ("RotateVertical");

		// Elevation input
		if (Input.GetButton ("Elevate"))
			elevation = elevationSpeed;
		if (Input.GetButton ("Sink"))
			elevation = (-1.0f) * elevationSpeed;

		// Position Update
		Vector3 movement = new Vector3 (0.0f, elevation, 0.0f);
		transform.position = transform.position + Camera.main.transform.forward * moveVertical * step;
		transform.position = transform.position + Camera.main.transform.right * moveHorizontal * step;
		transform.position = transform.position + movement * step;

		// Rotation Update
/*		Quaternion deltaRotation = Quaternion.Euler(0.0f, rotateHorizontal * rotateSpeed * Time.deltaTime, 0.0f);
//		print ("deltaRotation:" + deltaRotation);
		rb.MoveRotation(rb.rotation * deltaRotation);
		transform.Rotate(rotateVertical*rotateSpeed*Time.deltaTime, 0.0f, 0.0f);
		transform.Rotate(0.0f, rotateHorizontal*rotateSpeed*Time.deltaTime, 0.0f);
*/
		transform.Rotate(rotateVertical*speed*Time.deltaTime, 0.0f, 0.0f);
		transform.Rotate(0.0f, rotateHorizontal*speed*Time.deltaTime, 0.0f);
	}
}