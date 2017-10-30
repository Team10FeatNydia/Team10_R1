using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeButton : MonoBehaviour
{
	void Start ()
	{
		GetComponentInChildren<Button> ().onClick.AddListener (Clear);
	}

	public void Clear()
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovementScript> ().enabled = true;
		EventManager.instance.ClearEvent ();
		Destroy (transform.parent.parent.gameObject);
	}
}
