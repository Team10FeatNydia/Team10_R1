using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventListName
{
    EVENT_1 = 0,
    EVENT_2 = 1,

    TOTAL = 2
}

[System.Serializable]
public class EventListInfo
{
    public EventListName eventListName;
    public GameObject eventPrefab;
}

public class EventPopOutWindow : MonoBehaviour 
{
    public List<EventListInfo> eventListInfoList = new List<EventListInfo>();

    //public GameObject eventCanvasObj;
	//private Canvas eventCanvas;

	void Start ()
	{
		//eventCanvas = eventCanvasObj.GetComponent<Canvas> ();
		//eventCanvas.enabled = false;
		GameObject.Find ("Player").GetComponent<PlayerMovementScript> ().enabled = true;
	}

	void OnTriggerEnter (Collider other)
	{
		//if (other.gameObject.tag == "Player") 
		//{
		//	eventCanvas.enabled = true;
		//	GameObject.Find ("Player").GetComponent<PlayerMovementScript> ().enabled = false;
		//}

        if (other.gameObject.tag == "Player")
        {
            Invoke("eventPop", 1);

            GameObject.Find("Player").GetComponent<PlayerMovementScript>().enabled = false;
        }
    }

	void OnTriggerExit (Collider other)
	{
		//eventCanvas.enabled = false;
		GameObject.Find ("Player").GetComponent<PlayerMovementScript> ().enabled = true;
        
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
        Debug.LogError("Cant find event : " + eventListName);

        return null;
    }

    public void eventPop()
    {

        GameObject Event = FindEventName((EventListName)Random.Range(0, (int)EventListName.TOTAL));
        Instantiate(Event);
        CancelInvoke();
    }

}
