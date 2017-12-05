using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour 
{

    public float posX;
    public float posY;

    // Use this for initialization
    void Start () 
	{
		if(PlayerStatSaver.mInstance.playerPosX != 0)
		{
			posX = PlayerStatSaver.mInstance.playerPosX;
			posY = PlayerStatSaver.mInstance.playerPosY;
		}
		else
		{
			PlayerStatSaver.mInstance.playerPosX = 640;
			PlayerStatSaver.mInstance.playerPosY = 220;
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
		PlayerStatSaver.mInstance.playerPosX = posX;
		PlayerStatSaver.mInstance.playerPosY = posY;
    }

    void loadData()
    {
		posX = PlayerStatSaver.mInstance.playerPosX;
		posY = PlayerStatSaver.mInstance.playerPosY;
        transform.position = new Vector3(posX , posY , 0f);
    }
}
