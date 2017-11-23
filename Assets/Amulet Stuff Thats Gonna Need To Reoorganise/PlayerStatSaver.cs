using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatSaver : MonoBehaviour {

    public static PlayerStatSaver mInstance;

    public float HP;
    public float maxHP;
    public float posX;
    public float posY;
    public float BGposX;
    public float BGposY;
    public int eventCleared;
    public int playerPoint;
    GameObject player;

	public List<CardDescription> cardDeckList = new List<CardDescription>();
    //public float MaxHP;

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
        maxHP = HP;  
    }
}
