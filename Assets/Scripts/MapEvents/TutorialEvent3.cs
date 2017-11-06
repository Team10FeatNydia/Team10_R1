﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEvent3 : MonoBehaviour
{
	public GameObject choiceSuccess;

	void Start ()
	{
		transform.GetChild (0).GetComponent<Button> ().onClick.AddListener (GainAmulet);
	}

	void GainAmulet ()
	{
		Instantiate (choiceSuccess);
		// TODO::Gain amulet
		Destroy (transform.parent.gameObject);
	}
}
