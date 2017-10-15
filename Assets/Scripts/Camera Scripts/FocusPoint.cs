using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPoint : MonoBehaviour 
{
	public GameObject player;
	public bool isFollowingPlayer;

	// Use this for initialization
	void Start () 
	{
		isFollowingPlayer = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isFollowingPlayer)
		{
			transform.position = player.transform.position;
		}
	}
	/*
	public Vector3 initialPosition;
	public Quaternion initialRotation;

	public void TogglePoint ()
	{
		if (isFollowingPlayer) 
		{
			if (!player.GetComponent<MovementScript> ().enabled) 
			{
				player.GetComponent<MovementScript> ().enabled = true;
			}
			isFollowingPlayer = false;
		} 
		else 
		{
			if (player.GetComponent<MovementScript> ().enabled) 
			{
				player.GetComponent<MovementScript> ().enabled = false;
			}
			isFollowingPlayer = true;
		}
	}*/

	public void LockTransform (Vector3 position, Quaternion rotation)
	{
		isFollowingPlayer = false;
		transform.position = position;
		transform.rotation = rotation;
	}

	public void UnlockTransform ()
	{
		isFollowingPlayer = true;
		transform.rotation = Quaternion.Euler (0, 0, 0);
	}
}
