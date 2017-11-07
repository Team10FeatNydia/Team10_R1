using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
