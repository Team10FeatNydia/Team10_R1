using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeButton : MonoBehaviour
{
	void Start ()
	{
		GetComponent<Button> ().onClick.AddListener (Clear); // add clear function to the button
	}

	public void Clear ()
	{
		EventManager.instance.ClearEvent (); // clear event
		Destroy (transform.root.gameObject); // destroy the whole hierarchy
	}
}
