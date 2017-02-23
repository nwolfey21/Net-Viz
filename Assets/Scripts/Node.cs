using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	public string id;
	public TextMesh nodeText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//node text always facing camera
		nodeText.transform.LookAt (Camera.main.transform);
		nodeText.transform.Rotate (0, 180, 0);
	}
}