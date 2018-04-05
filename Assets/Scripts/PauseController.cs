using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {
	public Transform PauseCanvas;
	public GameController GController;

	private int delay;
	private bool loaded = false;
    private bool loadedSampling = false;
	private int layout = -1;
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
            if( loadedSampling == false)
            {
                //StartCoroutine(GController.LoadSamplingData());
                loadedSampling = true;
            }
			if (loadedSampling == true && loaded == false) {
				StartCoroutine (GController.LoadLayout (layout));
				loaded = true;
			}
		}
	}

	public void ClickExit ()
    {
		Application.Quit();
	}

    public void FatTreeToggleForceChanged( bool state )
    {
        if (state)
            layout = 1;            
    }

    public void FatTreeToggleStructuredChanged(bool state)
    {
        if (state)
            layout = 0;
    }

    public void SlimFlyToggleForceChanged( bool state )
    {
        if (state)
            layout = 2;
    }

    public void SlimFlyToggleStructuredChanged(bool state)
    {
        if (state)
            layout = 3;
    }
}
