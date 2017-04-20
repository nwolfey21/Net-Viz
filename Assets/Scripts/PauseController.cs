using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {
	public Transform PauseCanvas;
	public GameController GController;

	private int delay;
	private bool loaded = false;
	// Use this for initialization
	void Start () {
		Time.timeScale = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Pause")) {
			Pause ();
		}
	}

	public void Pause() {
		if (PauseCanvas.gameObject.activeInHierarchy == false) {
			PauseCanvas.gameObject.SetActive(true);
			Time.timeScale = 0;
		} else {
			PauseCanvas.gameObject.SetActive(false);
			Time.timeScale = 1;
			if (loaded == false) {
				StartCoroutine (GController.LoadLayout ());
				loaded = true;
			}
		}
	}

	public void ClickExit () {
		Application.Quit();
	}
}
