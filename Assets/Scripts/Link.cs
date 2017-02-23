﻿using System.Collections;
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

	private LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
		lineRenderer = gameObject.AddComponent<LineRenderer>();

		//color link according to status
		Color c;
		if (status == "Up")
			c = Color.gray;
		else
			c = Color.red;
		c.a = 0.5f;

		//draw line
		lineRenderer.material = new Material (Shader.Find("Self-Illumin/Diffuse"));
		lineRenderer.material.SetColor ("_Color", c);
		lineRenderer.startWidth = 0.3f;
		lineRenderer.endWidth = 0.3f;
		lineRenderer.numPositions = 2;
		lineRenderer.SetPosition(0, new Vector3(0,0,0));
		lineRenderer.SetPosition(1, new Vector3(1,0,0));
	}

	// Update is called once per frame
	void Update () {
		if(source && target && !loaded){
			//draw links as full duplex, half in each direction
			Vector3 m = (target.transform.position - source.transform.position)/2 + source.transform.position;
			lineRenderer.SetPosition(0, source.transform.position);
			lineRenderer.SetPosition(1, m);

			loaded = true;
		}
	}
}
