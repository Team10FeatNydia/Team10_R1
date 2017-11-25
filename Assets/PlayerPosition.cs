﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour {

    public float posX;
    public float posY;

    // Use this for initialization
    void Start () {
        loadData();
    }
	
	// Update is called once per frame
	void Update () {
        posX = transform.position.x;
        posY = transform.position.y;
        saveData();
	}

    void saveData()
    {
        PlayerStatSaver.mInstance.posX = posX - 640;
        PlayerStatSaver.mInstance.posY = posY - 480;
    }

    void loadData()
    {
        posX = PlayerStatSaver.mInstance.posX  +640;
        posY = PlayerStatSaver.mInstance.posY + 480;
        transform.position = new Vector3(posX , posY , 0f);
    }
}