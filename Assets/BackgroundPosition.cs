using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPosition : MonoBehaviour {

    public float posX;
    public float posY;

    // Use this for initialization
    void Start()
    {
        loadData();
    }

    // Update is called once per frame
    void Update()
    {
        posY = transform.position.y;
        saveData();
    }

    void saveData()
    {
       // PlayerStatSaver.mInstance.BGposY = player.posY;
        PlayerStatSaver.mInstance.BGposY = posY;
        //PlayerStatSaver.mInstance.BGposY = posY - 1188;
    }

    void loadData()
    {
        // posY = player.posY;
        posX = PlayerStatSaver.mInstance.BGposX;
        posY = PlayerStatSaver.mInstance.BGposY;
        //posY = PlayerStatSaver.mInstance.BGposY + 1188;
        transform.position = new Vector3(posX, posY, 0f);
    }
}
