using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCanvasScript : MonoBehaviour 
{
	public static BattleCanvasScript Instance;

    // Use this for initialization
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update () 
	{
//        if (GameManagerScript.Instance.curState != GameStates.BATTLE)
//        {
//            this.gameObject.SetActive(false);
//        }
    }
}
