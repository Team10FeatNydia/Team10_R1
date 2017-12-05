using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatSaver : MonoBehaviour 
{
    public static PlayerStatSaver mInstance;

    public float playerHP;
	public float playerMaxHP;
	public float playerPosX;
	public float playerPosY;
	public float playerBGposX;
	public float playerBGposY;
	public int playerEventCleared;
    public int playerPoint;
    GameObject player;

	//Change it back to 0, if not 0
	public int combatScene = 2;

	public List<CardDescription> cardDeckList = new List<CardDescription>();

    void Awake()
    {
        if (mInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            mInstance = this;
        }
        else if (mInstance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
		playerMaxHP = playerHP;
    }
}
