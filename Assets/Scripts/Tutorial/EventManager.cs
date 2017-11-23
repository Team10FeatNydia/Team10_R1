using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventName
{
	STARTING_POINT = 0,
	COMBAT_POINT = 1,
	HEALING_POINT = 2,
	SUB_COMBAT_POINT = 3,
	AMULET1_POINT = 4,
	AMULET2_POINT = 5,
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
	public GameObject arrowObject;
	public List<EventInformation> eventInformationList = new List<EventInformation> ();

	//private GameObject player;
	EventCollider curCollider;
	Arrow arrow;

	private void Awake ()
	{
		instance = this;
	}

	private void Start ()
	{
		//player = GameObject.Find ("Player");
		arrow = arrowObject.GetComponent<Arrow> ();
        SetClearedEventNumber(PlayerStatSaver.mInstance.eventCleared);

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
		int index = (int)curCollider.eventName;
		eventInformationList [index].isCleared = true;
        GetClearedEventNumber();
		Invoke ("ResetTrigger", 3.0f);
		path.GetComponent<MovementPath> ().enabled = true;
		Debug.Log ("Event done!");

		if (index <= 2) // amulet2 doesnt have to change the position of the arrow anymore
		{
			arrow.index = index;
			arrow.hide = false; // show the arrow 
		}
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
			if (index == 0 || (index > 0 && eventInformationList [index - 1].isCleared)) // first event doesnt have to check if previous event is done || if index > 0 then need to check if prev event (index-1) cleared
			{
				// if can start event
				path.GetComponent<MovementPath> ().enabled = false; // prevent player from moving
				Instantiate (eventInformationList [index].eventPrefab); // pop up the event canvas
				arrow.hide = true; // hide arrow
				return;
			}
			else // if previous events not done
			{
				Debug.Log ("Previous event has not done yet.");
			}
		}

		Invoke ("ResetTrigger", 3.0f);
	}

    public int GetClearedEventNumber()
    {
        int counter = 0;
        for (int i = 0; i < eventInformationList.Count; i++)
        {
            if (eventInformationList[i].isCleared)
            {
                counter++;
            }
        }
        return PlayerStatSaver.mInstance.eventCleared = counter;
    }

    public void SetClearedEventNumber(int n)
    {
        for (int i = 0; i < n; i++)
        {
            eventInformationList[i].isCleared = true;
        }
    }
}