using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatSaver : MonoBehaviour {

    public static PlayerStatSaver mInstance;

    public float HP;

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
    }

    // Update is called once per frame
    void Update () 
	{
		
	}
}
