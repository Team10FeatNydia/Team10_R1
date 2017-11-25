using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelScript : MonoBehaviour 
{
    public static PlayerLevelScript instance;

    public float posX;
    public float posY;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () 
	{
        LoadData();
    }
	
	// Update is called once per frame
	void Update () 
	{
        SaveData();
        posX = GetComponent<RectTransform>().position.x;
        posY = GetComponent<RectTransform>().position.y;
    }

    public void SaveData()
    {
		PlayerStatSaver.mInstance.playerPosX = posX;
		PlayerStatSaver.mInstance.playerPosY = posY;
    }

    public void LoadData()
    {
		posX = PlayerStatSaver.mInstance.playerPosX;
		posY = PlayerStatSaver.mInstance.playerPosY;
    }
}
