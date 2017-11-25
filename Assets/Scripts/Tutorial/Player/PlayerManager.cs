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
	public Animator animator;

	[Header("Developer")]
	public PlayerStatusScript status;
	public PlayerUIScript UI;

	[Header("Respawn")]
	public string respawnScene;
	public string quitScene;

	void Awake()
	{
		animator = GetComponentInChildren<Animator>();

		status = GetComponent<PlayerStatusScript>();
		UI = GetComponentInChildren<PlayerUIScript>();

		if(status 		!= null) status.self 		= this;
		if(UI			!= null) UI.self			= this;
	}
}