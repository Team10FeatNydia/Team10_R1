using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventName
{
	EVENT_1 = 0,
	EVENT_2 = 1,
	SUB_EVENT_1 = 2,
	EVENT_3 = 3,
	SUB_EVENT_2 = 4,
	EVENT_4 = 5,

	TOTAL = 6
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
	public GameObject path;
	public List<EventInformation> eventInformationList = new List<EventInformation>();

	//private GameObject player;
	private EventCollider curCollider;

	private void Awake ()
	{
		instance = this;
	}

	private void Start ()
	{
		//player = GameObject.Find ("Player");
	}

	public void StartEvent (EventCollider collider)
	{
		if (IsInvoking ())
		{
			ResetTrigger ();
		}

		curCollider = collider;
		curCollider.SetColliderActive (false);
		InitEvent ();
	}

	public void ClearEvent ()
	{
		eventInformationList [(int)curCollider.eventName].isCleared = true;
		//player.GetComponent<PlayerMovementScript> ().enabled = true;
		path.GetComponent<MovementPath> ().enabled = true;
		Debug.Log ("Event done!");
		Invoke ("ResetTrigger", 3.0f);
		//curCollider.transform.parent.GetComponent<MeshRenderer> ().material.color = Color.gray;
	}

	public void ResetTrigger ()
	{
		curCollider.SetColliderActive (true);
		CancelInvoke ();
	}

	public void InitEvent ()
	{
		int index = (int)curCollider.eventName;
		if (eventInformationList [index].isCleared) 
		{
			Debug.Log ("This event has already cleared!");
		}
		else
		{
			if (index == 0 || (index > 0 && eventInformationList [index - 1].isCleared))
			{
				//player.GetComponent<PlayerMovementScript> ().enabled = false;
				path.GetComponent<MovementPath> ().enabled = false;
				Instantiate (eventInformationList [index].eventPrefab);
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
