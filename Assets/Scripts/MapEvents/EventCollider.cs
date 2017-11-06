using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCollider : MonoBehaviour
{
	public EventName eventName;
	float timer = 0.0f;

	void Update ()
	{
		if (timer >= 1.0f)
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
