using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCollider : MonoBehaviour
{
	public EventName eventName;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			EventManager.instance.StartEvent (this);
		}
	}

	public void SetColliderActive (bool _bool)
	{
		GetComponent<BoxCollider> ().enabled = _bool;
	}
}
