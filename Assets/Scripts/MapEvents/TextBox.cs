using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
	public GameObject eventCanvas;

	void Start ()
	{
		transform.GetChild (0).GetComponent<Button> ().onClick.AddListener (ChangeEventCanvas);
	}

	void ChangeEventCanvas ()
	{
		Instantiate (eventCanvas);
		Destroy (transform.parent.gameObject);
	}
}
