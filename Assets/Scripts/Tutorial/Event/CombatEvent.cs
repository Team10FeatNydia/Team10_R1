using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatEvent : MonoBehaviour
{
	void Start ()
	{
		transform.GetChild (0).GetComponent<Button> ().onClick.AddListener (ArenaScene);
	}

	public void ArenaScene ()
	{
		GameManagerInstance.instance.ChangeScene (2);
	}
}
