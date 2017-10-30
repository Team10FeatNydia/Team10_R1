using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Padlock : MonoBehaviour 
{
	private int index;

	void Start () 
	{
		index = (int)GetComponentInParent<EventCollider> ().eventName;
	}

	void Update () 
	{
		if (EventManager.instance.eventInformationList [index - 1].isCleared)
		{
			Destroy (gameObject, 1.1f);
		}
	}
}	
