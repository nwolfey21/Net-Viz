using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSwitches : MonoBehaviour {

	public GameObject s;
	public int pods = 11;
	public int k = 18;
	public float levelDisplacement = 4.0f;

	// Use this for initialization
	void Start () {
		float offset = 0.0f;
		for (int l = 0; l < 2; l++) {
			for (int p = 0; p < pods; p++) {
				for (int i = 0; i < k; i++) {
					if (i == 0)
						offset = 2.5f;
					else
						offset = 0.0f;
//					Instantiate (s, new Vector3 (i*1.2f + p*(float)k*1.2f + offset, levelDisplacement + l*levelDisplacement, 0.0f), Quaternion.identity);
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
