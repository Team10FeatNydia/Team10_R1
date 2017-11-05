using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEvent1 : MonoBehaviour
{
	public GameObject choiceASuccess;
	public GameObject choiceAFail;
	public GameObject choiceB;

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
			Instantiate (choiceASuccess);
			// TODO::Heal 6 HP
		}

		Destroy (transform.parent.gameObject);
	}

	void WarpWound ()
	{
		Instantiate (choiceB);
		// TODO::Heal 2 HP
		Destroy (transform.parent.gameObject);
	}
}
