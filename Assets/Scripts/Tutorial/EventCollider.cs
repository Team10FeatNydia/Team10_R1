using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCollider : MonoBehaviour
{
	public EventName eventName;
	float timer = 0.0f;

	public bool activeAtStart;

	void Start ()
	{
		if (activeAtStart)
		{
			EventManager.instance.StartEvent (this); // spawn starting point canvas at start
		}
	}

	void Update ()
	{
		if (timer >= 0.2f) // start event only after player collide with the collider for 1s
		{
			EventManager.instance.StartEvent (this); 
			timer = 0.0f;
		}
	}

	public void SetColliderActive (bool active)
	{
		GetComponent<BoxCollider2D> ().enabled = active;
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			timer += Time.deltaTime;
		}
	}
}
