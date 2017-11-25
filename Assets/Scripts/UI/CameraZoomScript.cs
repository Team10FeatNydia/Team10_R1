using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomScript : MonoBehaviour {

	public int zoom = 25;
	public int normal = 60;
	public float ease = 3;
	public float delay;
	public GameObject endGameMenu;

	public bool isZoomed = false;

	// Use this for initialization
	void Start () {	
		SoundManagerScript.Instance.PlayBGM (AudioClipID.BGM_END_GAME);
		Invoke ("zoomTrue", delay);
		Invoke ("test", 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (isZoomed) {
			GetComponent<Camera> ().fieldOfView = Mathf.Lerp (GetComponent<Camera> ().fieldOfView, zoom, Time.deltaTime * ease);
		}
		else 
		{
			GetComponent<Camera> ().fieldOfView = Mathf.Lerp (GetComponent<Camera> ().fieldOfView, normal, Time.deltaTime * ease);
		}
	}

	void test()
	{
		FadeManagerScript.Instance.fadeIn ();
		endGameMenu.SetActive (true);

	}
		

	void zoomTrue()
	{
		isZoomed = true;
		FadeManagerScript.Instance.fadeOut ();
	}
}
