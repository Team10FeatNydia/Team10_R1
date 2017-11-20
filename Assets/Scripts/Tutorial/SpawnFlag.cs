using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlag : MonoBehaviour
{
	public GameObject flag;
	public bool isSpawned = false;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (!isSpawned && other.CompareTag ("Player"))
		{
			flag.SetActive (true); // show the flag
			isSpawned = true;
		}
	}
}