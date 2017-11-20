﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthEvent : MonoBehaviour
{
	public GameObject choiceASuccess;
	public GameObject choiceAFail;
	public GameObject choiceB;
	public GameObject healthFull;

	void Start ()
	{
		transform.GetChild (0).GetComponent<Button> ().onClick.AddListener (HealingCard);
		transform.GetChild (1).GetComponent<Button> ().onClick.AddListener (WarpWound);
	}

	void HealingCard ()
	{
		int rand = Random.Range (0, 2);

		if (rand == 0) // If fail
		{
			Instantiate (choiceAFail);
		}
		else // If success
		{
			Heal (10, true);
		}

		Destroy (transform.root.gameObject);
	}

	void WarpWound ()
	{
		Heal (4, false);
		Destroy (transform.root.gameObject);
	}

	void Heal (int amount, bool isChoiceA)
	{
		GameData gameData = GameManagerInstance.instance.gameData; // Load game data from game manager instance
		if (gameData.playerHP >= gameData.playerMaxHP) // If player has full health
		{
			Instantiate (healthFull);
		}
		else
		{
			gameData.playerHP += amount;

			if (gameData.playerHP > gameData.playerMaxHP)
			{
				gameData.playerHP = gameData.playerMaxHP;
			}

			if (isChoiceA)
			{
				Instantiate (choiceASuccess);
			}
			else
			{
				Instantiate (choiceB);
			}
		}
	}
}