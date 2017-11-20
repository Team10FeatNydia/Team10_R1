using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatEvent : MonoBehaviour
{
	void Start ()
	{
		transform.GetChild (0).GetComponent<Button> ().onClick.AddListener (CardDeckDisplay);
	}

	void CardDeckDisplay()
	{
		CardManagerScript.Instance.DisplayDeckPanels();
		transform.parent.GetComponent<Canvas>().enabled = false;
	}

	//Change Scene function changed to CardManagerScript.ChangeScene()
	public void ArenaScene ()
	{
		//GameManagerInstance.instance.ChangeScene (4);
	}
}
