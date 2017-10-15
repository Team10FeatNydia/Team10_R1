using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneList
{
	public List<string> keyScene;
	public List<string> valueScene;

	public SceneList()
	{
		keyScene = new List<string>();
		valueScene = new List<string>();
	}

	public bool ContainsKey(string key)
	{
		return keyScene.Contains(key);
	}

	public string GetValue(string key)
	{
		if(ContainsKey(key))
		{
			return valueScene[keyScene.IndexOf(key)];
		}
		else
		{
			return null;
		}
	}
}

public class PlayerManager : MonoBehaviour 
{
	[Header("System")]
	public new CapsuleCollider collider;
	public new Rigidbody rigidbody;
	public new SpriteRenderer renderer;
	public Animator animator;

	[Header("Developer")]
	public PlayerMovementScript movement;
	public PlayerStatusScript status;
	public PlayerUIScript ui;

	[Header("Respawn")]
	public string respawnScene;
	public string quitScene;

	void Awake()
	{
		collider = GetComponent<CapsuleCollider>();
		rigidbody = GetComponent<Rigidbody>();
		renderer = GetComponentInChildren<SpriteRenderer>();
		animator = GetComponentInChildren<Animator>();

		movement = GetComponent<PlayerMovementScript>();
		status = GetComponent<PlayerStatusScript>();
		ui = GetComponentInChildren<PlayerUIScript>();

		if(movement 	!= null) movement.self 		= this;
		if(status 		!= null) status.self 		= this;
		if(ui			!= null) ui.self			= this;
	}

	/*
	public void EnableControls()
	{
		controls.enabled = true;
	}

	public void DisableControls()
	{
		controls.enabled = false;
		controls.moveSpeedFactor = 0.0f;
		// SoundScript here
		animator
	}
	*/
}