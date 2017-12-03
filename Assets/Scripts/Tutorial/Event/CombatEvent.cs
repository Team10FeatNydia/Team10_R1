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
		//CardManagerScript.Instance.DisplayDeckPanels();
		transform.parent.parent.parent.GetComponent<Canvas>().enabled = false;
        EventManager.instance.ClearEvent();

		if(PlayerStatSaver.mInstance.combatScene == 0)
		{
			PlayerStatSaver.mInstance.combatScene ++;
			GameManagerInstance.instance.ChangeScene (2);
		}
		else if(PlayerStatSaver.mInstance.combatScene == 1)
		{
			PlayerStatSaver.mInstance.combatScene ++;
			GameManagerInstance.instance.ChangeScene (3);
		}
		else if(PlayerStatSaver.mInstance.combatScene == 2)
		{
			PlayerStatSaver.mInstance.combatScene ++;
			GameManagerInstance.instance.ChangeScene (5);
		}
			

	}

	//Change Scene function changed to CardManagerScript.ChangeScene()
	public void ArenaScene ()
	{
		//GameManagerInstance.instance.ChangeScene (2);
	}
}
