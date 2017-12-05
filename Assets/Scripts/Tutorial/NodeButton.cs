using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeButton : MonoBehaviour
{
	void Start ()
	{
		GetComponent<Button> ().onClick.AddListener (Clear);
	}

	public void Clear ()
	{
		EventManager.instance.ClearEvent ();
		Destroy (transform.root.gameObject);
	}
}
