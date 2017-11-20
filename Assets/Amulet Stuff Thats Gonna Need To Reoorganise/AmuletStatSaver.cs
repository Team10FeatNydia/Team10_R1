using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD:Assets/PlayerStatSaver.cs
public class PlayerStatSaver : MonoBehaviour {
	
	public static PlayerStatSaver mInstance;
	public float HP;

	void Awake () 
	{
		if (mInstance == null)
		{
			mInstance = this;
		}
		else if (mInstance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad (this);
	}

	void Start ()
	{
		HP = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
=======
public class AmuletStatSaver : MonoBehaviour {

    public static AmuletStatSaver mInstance;

    public bool combatAmuletActive;

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

    void Start()
    {
        combatAmuletActive = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
>>>>>>> 8a9401ff761d541b7d98dc56506e7996a2d238e5:Assets/Amulet Stuff Thats Gonna Need To Reoorganise/AmuletStatSaver.cs
}
