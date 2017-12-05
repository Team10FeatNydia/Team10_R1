using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmuletEvent : MonoBehaviour
{
	public GameObject choiceSuccess;

	void Start ()
	{
		transform.GetChild (0).GetComponent<Button> ().onClick.AddListener (GainAmulet);
	}

	void GainAmulet ()
	{
		EventManager.instance.ClearEvent ();

        AmuletStatSaver.mInstance.combatAmuletActive = true;

        EventManager.instance.ClearEvent ();
		EventManager.instance.path.GetComponent<MovementPath> ().MoveToAmuletPlace ();
		Destroy (transform.parent.parent.parent.gameObject);
	}
}
