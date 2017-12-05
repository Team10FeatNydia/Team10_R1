using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventListName
{
    EVENT_1 = 0,
    EVENT_2 = 1,
	EVENT_3 = 2,

    TOTAL = 3
}

[System.Serializable]

public class EventListInfo
{
    public EventListName eventListName;
    public GameObject eventPrefab;
	public bool isCleared = false;
}

public class EventPopOutWindow : MonoBehaviour 
{
	public List<EventListInfo> eventListInfoList = new List<EventListInfo>();
	
	private GameObject player;

	void Start ()
	{
		player = GameObject.Find ("Player");
		player.GetComponent<PlayerMovementScript> ().enabled = true;		
	}

	void OnTriggerEnter (Collider other)
	{
        if (other.gameObject.tag == "Player")
        {
            Invoke("eventPop", 1); // Delay 1s before invoking method

            player.GetComponent<PlayerMovementScript>().enabled = false;
        }
    }

	void OnTriggerExit (Collider other)
	{
		//eventCanvas.enabled = false;
		player.GetComponent<PlayerMovementScript> ().enabled = true;
        
	}

    GameObject FindEventName(EventListName eventListName)
    {
        for(int i = 0; i < eventListInfoList.Count; i++)
        {
            if(eventListInfoList[i].eventListName == eventListName)
            {
                return eventListInfoList[i].eventPrefab;
            }
        }
        return null;
    }

    public void eventPop()
    {
		GameObject eventObj = FindEventName(EventListName.EVENT_1);
		Instantiate(eventObj);
        CancelInvoke();
    }
}
