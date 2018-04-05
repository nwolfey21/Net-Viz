using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour {

	public string id;
	public Node source;
	public Node target;
	public string sourceId;
	public string targetId;
	public string status;
	public bool loaded = false;
	public bool colorChange = false;

	private Color linkColor;
	private int src;
	private int dst;

	private LineRenderer lineRend;

	// Use this for initialization
	void Start () {
		lineRend = gameObject.GetComponent<LineRenderer>();

		//color link according to status
		Color c;
		if (status == "Up")
			c = Color.blue;
		else
			c = Color.red;
		c.a = 0.001f;

		//draw line
		//lineRend.material = new Material (Shader.Find("Self-Illumin/Diffuse"));
		//lineRend.material.SetColor ("_Color", c);
		lineRend.startWidth = 0.05f;
		lineRend.endWidth = 0.05f;
//		lineRend.positionCount = 2;
		lineRend.SetPosition(0, new Vector3(0,0,0));
		lineRend.SetPosition(1, new Vector3(1,0,0));

        //If layout = 0
        //string[] tmp = sourceId.Split('_'); 
        //src = int.Parse (tmp [1]);
        //If layout = 1
        src = int.Parse(sourceId);
//		print ("src: " + src);
	}

	// Update is called once per frame
	void Update () {
		if(source && target && !loaded){
			//draw links as full duplex, half in each direction
			Vector3 m = (target.transform.position - source.transform.position) + source.transform.position;
			lineRend.SetPosition(0, source.transform.position);
			lineRend.SetPosition(1, m);

			loaded = true;
		}
		//if (loaded == true && colorChange == true/*&& src < 4000*/) {
			/*linkColor.r += 1.0f / 255.0f;
			linkColor.g += 2.0f / 255.0f;
			linkColor.b += 3.0f / 255.0f;
			if (linkColor.r > 1.0f)
				linkColor.r = 0.0f;
			if (linkColor.g > 1.0f)
				linkColor.g = 0.0f;
			if (linkColor.g > 1.0f)
				linkColor.g = 0.0f;
			
//			print (" r" + linkColor.r);
//			lineRend.material.SetColor (linkColor);
			lineRend.material.color = linkColor;*/
		//}
	}
}
