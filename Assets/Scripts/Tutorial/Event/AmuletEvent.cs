﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmuletEvent : MonoBehaviour
{
	public GameObject choiceSuccess;

	void Start ()
	{
		transform.GetChild (0).GetComponent<Button> ().onClick.AddListener (GainAmulet); //Assign gain amulet func in first button
	}

	void GainAmulet ()
	{
<<<<<<< HEAD
		Debug.Log("Got Amulet");

		// Instantiate (choiceSuccess);
		// TODO::Gain amulet
		EventManager.instance.ClearEvent ();
=======
        // Instantiate (choiceSuccess);
        // TODO::Gain amulet
        AmuletStatSaver.mInstance.combatAmuletActive = true;
        EventManager.instance.ClearEvent ();
>>>>>>> origin/ResolutionFixLevel
		EventManager.instance.path.GetComponent<MovementPath> ().MoveToAmuletPlace ();
		Destroy (transform.parent.parent.parent.gameObject);
	}
}
