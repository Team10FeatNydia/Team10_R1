using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatSaver : MonoBehaviour {

    public static PlayerStatSaver mInstance;

    public float HP;
    public float posX;
    public float posY;
    public float BGposX;
    public float BGposY;
    GameObject player;

	public List<CardDescription> cardHandList = new List<CardDescription>();
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
        HP = 100;
        player = GameObject.FindGameObjectWithTag("Player");
       
    }

    // Update is called once per frame
    void Update () 
	{
		
	}
}
