using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleStates
{
	PLAYER_TURN,
	CHOOSE_CARDS,
	CHOOSE_ENEMIES,
	ENEMY_TURN,
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

	[Header("Battle Settings")]
	public PlayerStatusScript player;
	public List<EnemyStatusScript> enemyList = new List<EnemyStatusScript>();
	public EnemyStatusScript target;
	public CardScript selectedCard;
	public CardPouchScript cardPouch;
    public BattleStates currTurn;
	public Canvas battleCanvas;
    int manaregen;
	public int enemyTurn = 0;
	public bool enemyAction = true;
	public Text manaText;
   
	[Header("WinLoseCanvas")]
	bool WinCondition = false;
	bool WinLoseGOSpawned = false;
	public GameObject WinLoseGO;
	public Text WinLoseText;
	public Text WinLoseComment;
	public Image newCardDisplay;
	public Text newCardText;

	public string[] winStrings;
	public string[] loseStrings;

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
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        manaregen = player.localPlayerData.manaPoints;
		manaText.text = player.localPlayerData.manaPoints.ToString();
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
        if (enemyList.Count == 0)
        {
			if(!WinLoseGOSpawned)
			{
				WinCondition = true;
				WinLoseGOSpawned = true;
				WinLoseGO.SetActive(true);

				WinLoseText.text = "You Win!";

				int rand = Random.Range(0, winStrings.Length);

				WinLoseComment.text = winStrings[rand];

				bool gotNewCard = false;

				newCardDisplay.enabled = false;

				if(!gotNewCard)
				{
					newCardDisplay.enabled = false;
					newCardText.text = "";
				}
				else
				{
					newCardDisplay.enabled = true;
					newCardText.text = "Obtained A New Card";

					WinLoseComment.text = "Hurray! You Got A Card! We Done Yet?";
				}

				FadeManagerScript.Instance.fadeOut();
			}
            //GameManagerInstance.instance.ChangeScene(0);
        }
		else if(PlayerStatSaver.mInstance.playerHP <= 0)
		{
			if(!WinLoseGOSpawned)
			{
				WinCondition = false;
				WinLoseGOSpawned = true;
				WinLoseGO.SetActive(true);

				WinLoseText.text = "You Lose!";

				int rand = Random.Range(0, loseStrings.Length);

				WinLoseComment.text = loseStrings[rand];

				newCardDisplay.enabled = false;
				newCardText.text = "";

				FadeManagerScript.Instance.fadeOut();

//				bool gotNewCard = false;
//
//				newCardDisplay.enabled = false;
//
//				if(!gotNewCard)
//				{
//					newCardDisplay.enabled = false;
//					newCardText.text = "";
//				}
//				else
//				{
//					newCardDisplay.enabled = true;
//					newCardText.text = "Obtained A New Card";
//				}
			}
			//GameManagerInstance.instance.ChangeScene(0);
		}
			


        if (currTurn == BattleStates.ENEMY_TURN)
        {
			if(enemyAction && enemyTurn < enemyList.Count)
			{
				enemyAction = false;
				enemyList[enemyTurn].CheckBehaviour();
				enemyTurn++; 
			}

			if(enemyAction && enemyTurn >= enemyList.Count)
			{
				Debug.Log("Enemies Done Attack");
				enemyAction = false;
				enemyTurn = 0;
				player.localPlayerData.manaPoints += manaregen;
				if (player.localPlayerData.manaPoints >= 15)
				{
					player.localPlayerData.manaPoints = 15;
				}
				manaText.text = player.localPlayerData.manaPoints.ToString();
				currTurn = BattleStates.PLAYER_TURN;

				Debug.Log("Enemy's Turn End");

			}
        }
        else if (currTurn == BattleStates.PLAYER_TURN)
        {

        }
		else if (currTurn == BattleStates.CHOOSE_CARDS)
		{

		}
		else if (currTurn == BattleStates.CHOOSE_ENEMIES)
		{
            player.isHit = false;
		}
    }

	public void EndCombatScene()
	{
		GameManagerInstance.instance.ChangeScene(1);
	}
}
