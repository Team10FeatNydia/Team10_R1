using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour 
{

    public float posX;
    public float posY;

    GameObject startPoint;

    // Use this for initialization
    void Start () 
	{
        startPoint = GameObject.FindGameObjectWithTag("StartPoint");

		if(PlayerStatSaver.mInstance.playerPosX != 0)
		{
			posX = PlayerStatSaver.mInstance.playerPosX;
			posY = PlayerStatSaver.mInstance.playerPosY;
		}
		else
		{
			PlayerStatSaver.mInstance.playerPosX =  startPoint.transform.position.x;
			PlayerStatSaver.mInstance.playerPosY = startPoint.transform.position.y;
        }

        loadData();
    }
	
	// Update is called once per frame
	void Update () 
	{
        posX = transform.position.x;
        posY = transform.position.y;
        
		saveData();
	}

    void saveData()
    {
//		PlayerStatSaver.mInstance.playerPosX = posX - 640;
//		PlayerStatSaver.mInstance.playerPosY = posY - 480;

		PlayerStatSaver.mInstance.playerPosX = posX;
		PlayerStatSaver.mInstance.playerPosY = posY;
    }

    void loadData()
    {

		posX = PlayerStatSaver.mInstance.playerPosX;
		posY = PlayerStatSaver.mInstance.playerPosY;

//		posX = PlayerStatSaver.mInstance.playerPosX + 640;
//		posY = PlayerStatSaver.mInstance.playerPosY + 480;
//
//        if (posX == 0)
//        {
//            posX = 40;
//            posY = 193;
//        }
        transform.position = new Vector3(posX , posY , 0f);
    }
}
