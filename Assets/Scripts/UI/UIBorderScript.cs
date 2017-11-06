using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBorderScript : MonoBehaviour {
	
	public float delay = 0.35f;

	// Use this for initialization
	void Start () {
		Invoke ("ManaActive", delay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ManaActive()
	{
		transform.GetChild (5).gameObject.SetActive (true);
	}
}
