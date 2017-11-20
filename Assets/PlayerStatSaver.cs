using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatSaver : MonoBehaviour {
	
	public static PlayerStatSaver mInstance;
	public float HP;

	void Awake () 
	{
		if (mInstance == null)
		{
			mInstance = this;
		}
		else if (mInstance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad (this);
	}

	void Start ()
	{
		HP = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
