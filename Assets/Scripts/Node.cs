using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	public string id;
	public TextMesh nodeText;
    public long[] sampleData;
    public int numSamples;

	private MeshRenderer meshRenderer;
	private Material shader;
	private Color nodeColor;
	private int lpID;
	private int iter;
	private float growth; 		// -1:shrinking, 1:growing

	// Use this for initialization
	void Start () {
		meshRenderer = gameObject.GetComponent<MeshRenderer> ();
		//meshRenderer.material = new Material (Shader.Find("Self-Illumin/Diffuse"));
		meshRenderer.material.SetColor ("_Color", Color.green);

        //If doing layout=0
        //string[] tmp = id.Split('_'); 
        //lpID = int.Parse (tmp [1]);
        //If doing layout=1
        lpID = int.Parse(id);
		iter = 1;
		growth = 0.0F; 
	}
	
	// Update is called once per frame
	void Update () {
		//node text always facing camera
		//nodeText.transform.LookAt (Camera.main.transform);
		//nodeText.transform.Rotate (0, 180, 0);

		/*if (lpID < 10000) {
			nodeColor.r += 3.0f / 255.0f;
			nodeColor.g += 2.0f / 255.0f;
			nodeColor.b += 1.0f / 255.0f;
			if (nodeColor.r > 1.0f)
				nodeColor.r = 0.0f;
			if (nodeColor.g > 1.0f)
				nodeColor.g = 0.0f;
			if (nodeColor.g > 1.0f)
				nodeColor.g = 0.0f;

//			meshRenderer.material.color = nodeColor;
			if (iter++ % 1000 == 0) {
				iter = 0;
				growth = growth * -1.0F;
			}
			meshRenderer.transform.localScale = meshRenderer.transform.localScale + new Vector3(0.01F* growth, 0.01F* growth, 0.01F* growth);
		}*/
	}
}