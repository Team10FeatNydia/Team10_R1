using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventName
{
	EVENT_1 = 0,
	EVENT_2 = 1,
	EVENT_3 = 2,
	EVENT_4 = 3,
	EVENT_5 = 4,

	TOTAL = 5
}

[System.Serializable]

public class EventInformation
{
	public EventName eventListName;
	public GameObject eventPrefab;
	public bool isCleared = false;
}

public class EventManager : MonoBehaviour
{
	public static EventManager instance;
	public List<EventInformation> eventInformationList = new List<EventInformation>();

	private GameObject player;
	private EventCollider curCollider;

	private void Awake ()
	{
		instance = this;
	}

	private void Start ()
	{
		player = GameObject.Find ("Player");
	}

	public void StartEvent (EventCollider collider)
	{
		if (IsInvoking ())
		{
			ResetTrigger ();
		}

		curCollider = collider;
		curCollider.SetColliderActive (false);
		InitEvent (curCollider.eventName);
	}

	public void ClearEvent ()
	{
		eventInformationList [(int)curCollider.eventName].isCleared = true;
		player.GetComponent<PlayerMovementScript> ().enabled = true;
		Debug.Log ("Event done!");
		Invoke ("ResetTrigger", 3.0f);
		curCollider.transform.parent.GetComponent<MeshRenderer> ().material.color = Color.gray;
	}

	public void ResetTrigger ()
	{
		curCollider.SetColliderActive (true);
		CancelInvoke ();
	}

	public void InitEvent (EventName name)
	{
		int index = (int)name;
		if (eventInformationList [index].isCleared) 
		{
			Debug.Log ("This event has already cleared!");
		}
		else
		{
			if (index == 0 || (index > 0 && eventInformationList[index - 1].isCleared))
			{
				player.GetComponent<PlayerMovementScript> ().enabled = false;
				Instantiate(eventInformationList [index].eventPrefab);
				return;
			}
			else
			{
				Debug.Log ("Previous event has not done yet.");
			}
		}

		Invoke ("ResetTrigger", 3.0f);
	}
}
