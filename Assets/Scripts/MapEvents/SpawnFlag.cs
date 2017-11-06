using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlag : MonoBehaviour
{
	public bool isSpawned = false;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (!isSpawned && other.CompareTag ("Player"))
		{
			transform.GetChild (0).gameObject.SetActive (true);
			isSpawned = true;
		}
	}
}
