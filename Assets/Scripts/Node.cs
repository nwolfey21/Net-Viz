using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	public string id;
	public TextMesh nodeText;

	private MeshRenderer meshRenderer;
	private Material shader;
	private Color nodeColor;
	private int lpID;

	// Use this for initialization
	void Start () {
		meshRenderer = gameObject.GetComponent<MeshRenderer> ();
		meshRenderer.material = new Material (Shader.Find("Self-Illumin/Diffuse"));
		meshRenderer.material.SetColor ("_Color", Color.green);

		string[] tmp = id.Split('_'); 
		lpID = int.Parse (tmp [1]);
	}
	
	// Update is called once per frame
	void Update () {
		//node text always facing camera
		nodeText.transform.LookAt (Camera.main.transform);
		nodeText.transform.Rotate (0, 180, 0);

		if (lpID < 100) {
			nodeColor.r += 3.0f / 255.0f;
			nodeColor.g += 2.0f / 255.0f;
			nodeColor.b += 1.0f / 255.0f;
			if (nodeColor.r > 1.0f)
				nodeColor.r = 0.0f;
			if (nodeColor.g > 1.0f)
				nodeColor.g = 0.0f;
			if (nodeColor.g > 1.0f)
				nodeColor.g = 0.0f;

			print (" r" + nodeColor.r);
			//			lineRenderer.material.SetColor (linkColor);
			meshRenderer.material.color = nodeColor;
		}
	}
}