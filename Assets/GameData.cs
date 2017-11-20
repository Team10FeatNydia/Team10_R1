using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
	[Header("Player Information")]
	public int playerMaxHP;
	public int playerHP;

	void Start ()
	{
		playerHP = playerMaxHP;
	}
}
