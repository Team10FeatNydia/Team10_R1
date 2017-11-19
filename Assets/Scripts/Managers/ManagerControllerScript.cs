﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerControllerScript : MonoBehaviour 
{
	#region Singleton
	private static ManagerControllerScript mInstance = null;

	public static ManagerControllerScript Instance
	{
		get
		{
			if(mInstance == null)
			{
				GameObject tempObject = GameObject.FindWithTag("GameController");

				if(tempObject == null)
				{
					//Debug.LogError("ManagerController is missing, the game cannot continue to work.");
					Debug.Break();
				}
				else
				{
					mInstance = tempObject.GetComponent<ManagerControllerScript>();
				}
			}
			return mInstance;
		}
	}
	public static bool CheckInstanceExist()
	{
		return mInstance;
	}
	#endregion Singleton

	[Header("Settings")]
	public GameObject gameManagerPrefab;
	public GameObject soundManagerPrefab;
	public GameObject pauseMenuManagerPrefab;
	public GameObject battleManagerPrefab;
	public GameObject cardManagerPrefab;
    public GameObject spellsManagerPrefab;

	[Header("In-Scene Managers")]
	public SoundManagerScript soundManager;
	public GameManagerScript gameManager;
	public BattleManagerScript battleManager;
	public CardManagerScript cardManager;
    public SpellsManagerScript spellsManager;
}
