using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatusScript : MonoBehaviour 
{
	[HideInInspector]
	public EnemyManager self;

	[Header("Stats")]
	public int health;
    public int maxHealth;
	public int attack;
	float posX;
	float posY;

	[Header("Combat")]
	public bool targeted;
	public PlayerStatusScript player;

	[Header("UI")]
	public bool showGUI;
	public Text hpText;
	public Text onTarget;

	public float easeTime; 

	void Start()
	{
		player = BattleManagerScript.Instance.player;
		targeted = false;
		posX = this.gameObject.transform.position.x;
		posY = this.gameObject.transform.position.y;
	}

	void Update()
	{
		if(PauseMenuManagerScript.Instance.paused) return;
	}

	public void Attack()
	{
		player.localPlayerData.health -= attack;
	}

	public void OnMouseDown()
	{
		Debug.Log("Click");
		if(BattleManagerScript.Instance.currTurn == BattleStates.PLAYER_TURN)
		{
			BattleManagerScript.Instance.target = this;
			GetComponent<MeshRenderer>().material.color = Color.white;
		}
	}

	public void CheckHealth()
	{
		if (health <= 0)
		{
			BattleManagerScript.Instance.enemyList.Remove(this);
			Destroy(this.gameObject);
		}
	}
}
