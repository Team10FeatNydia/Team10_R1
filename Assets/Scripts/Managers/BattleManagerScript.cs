using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleStates
{
	PLAYER_TURN,
	ENEMY_TURN
}

public class BattleManagerScript : MonoBehaviour 
{
	#region Singleton
	private static BattleManagerScript mInstance;

	public static BattleManagerScript Instance
	{
		get
		{
			if(mInstance == null)
			{
				GameObject temp = GameObject.FindGameObjectWithTag("BattleManager");

				if(temp == null)
				{
					Instantiate(ManagerControllerScript.Instance.battleManagerPrefab, Vector3.zero, Quaternion.identity);
				}
				mInstance = temp.GetComponent<BattleManagerScript>();
			}
			return mInstance;
		}
	}

	public static bool CheckInstanceExit()
	{
		return mInstance;
	}
	#endregion Singleton

	public PlayerStatusScript player;
	public List<EnemyStatusScript> enemyList = new List<EnemyStatusScript>();
	public EnemyStatusScript target;
    //public Button attackButton;
    public BattleStates currTurn;
	public Canvas battleCanvas;
    int manaregen;


    void Awake() 
	{
		if(BattleManagerScript.CheckInstanceExit())
		{
			Destroy(this.gameObject);
		}
    }

	// Use this for initialization
	void Start () 
	{
		manaregen = player.localPlayerData.manaPoints;
		GameObject[] enemies;
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
        currTurn = BattleStates.PLAYER_TURN;

		for(int i = 0; i < enemies.Length; i++)
		{
			enemyList.Add(enemies[i].GetComponent<EnemyStatusScript>());
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if(PauseMenuManagerScript.Instance.paused) return;

		

        if (currTurn != BattleStates.PLAYER_TURN)
        {
			for(int i = 0; i < enemyList.Count; i++)
			{
				enemyList[i].Attack();
                
			}
			player.localPlayerData.manaPoints += manaregen;
			if (player.localPlayerData.manaPoints >= 15)
            {
				player.localPlayerData.manaPoints = 15;
            }
            currTurn = BattleStates.PLAYER_TURN;
        }
        else if (currTurn == BattleStates.PLAYER_TURN)
        {

        }
    }


}
