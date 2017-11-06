using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
	NORMAL,
	KNIGHT,
	QUEEN,
	KING,
}

public class EnemyStatusScript : MonoBehaviour
{
	[HideInInspector]
	public EnemyManager self;

	[Header("Stats")]
	public EnemyType myType;
	public int health;
    public int maxHealth;
	public int attack;
	float posX;
	float posY;
	public bool stunned;
	public bool interactable;

	[Header("Combat")]
	public bool targeted;
	public PlayerStatusScript player;

	[Header("UI")]
	public bool showGUI;
	public Text hpText;
	public Text onTarget;

	public float easeTime; 
	public ParticleSystem blueTarget;
	public ParticleSystem redTarget;
	public ParticleSystem swordAttackPlayer;
	//public ParticleSystem gotHitEnemy;
	public ParticleSystem deathEffectEnemy;

	void Start()
	{
		player = BattleManagerScript.Instance.player;
		targeted = false;
		posX = this.gameObject.transform.position.x;
		posY = this.gameObject.transform.position.y;
        CheckAmulet();
    }

	void Update()
	{
		if(PauseMenuManagerScript.Instance.paused) return;
	}

	public void Attack(int damage)
	{
		player.enemyAttack.Play();
		player.localPlayerData.health -= damage;

	}

	public void HeavyAttack(int damage)
	{
		player.enemyHeavyAttack.Play();
		player.localPlayerData.health -= damage;
	}

	public void OnMouseDown()
	{
		Debug.Log("Click");
		if(BattleManagerScript.Instance.currTurn == BattleStates.PLAYER_TURN)
		{
			BattleManagerScript.Instance.target = this;
			GetComponent<MeshRenderer>().material.color = Color.white;
		}
		else if(BattleManagerScript.Instance.currTurn == BattleStates.CHOOSE_ENEMIES)
		{
			if(interactable)
			{
				if(BattleManagerScript.Instance.selectedCard != null)
				{
					if(BattleManagerScript.Instance.selectedCard.target != null)
					{
						BattleManagerScript.Instance.selectedCard.target.redTarget.Stop();
						BattleManagerScript.Instance.selectedCard.target.blueTarget.Play();
					}

					BattleManagerScript.Instance.selectedCard.target = this;
					BattleManagerScript.Instance.selectedCard.myImage.color = Color.white;

					//				for(int i = 0; i < BattleManagerScript.Instance.enemyList.Count; i++)
					//				{
					//					BattleManagerScript.Instance.enemyList[i].redTarget.Stop();
					//				}

					redTarget.Play();
				}
			}
		}
	}

    void CheckAmulet()
    {
        if (AmuletStatSaver.mInstance.combatAmuletActive)
        {
            health -= (maxHealth * 15 / 100);
        }
    }

	public void CheckHealth()
	{
		if (health <= 0)
		{
			deathEffectEnemy.Play();
			BattleManagerScript.Instance.enemyList.Remove(this);
			Destroy(this.gameObject, 2);
		}
	}

	public void CheckBehaviour()
	{
		StopCoroutine("PerformBehaviour");
		StartCoroutine("PerformBehaviour");
	}

	IEnumerator PerformBehaviour()
	{
		if(!stunned)
		{
			if(myType == EnemyType.NORMAL)
			{
				int rand = Random.Range(1, attack + 1);

				Attack(rand);

				yield return new WaitForSeconds(2f);

				BattleManagerScript.Instance.enemyAction = true;
			}
			else if(myType == EnemyType.KNIGHT)
			{
				int rand;
				bool kingless = true;

				for(int i = 0; i < BattleManagerScript.Instance.enemyList.Count; i++)
				{
					if(BattleManagerScript.Instance.enemyList[i] != null)
					{
						if(BattleManagerScript.Instance.enemyList[i].myType == EnemyType.KING)
						{
							if(BattleManagerScript.Instance.enemyList[i].health < (20/100*(BattleManagerScript.Instance.enemyList[i].maxHealth)))
							{
								kingless = false;
								rand = Random.Range(attack, (int)(attack * 1.5));
								HeavyAttack(rand);
								break;
							}
						}
					}
				}

				if(kingless)
				{
					rand = Random.Range(1, attack + 1);
					Attack(rand);
				}

				yield return new WaitForSeconds(2f);

				BattleManagerScript.Instance.enemyAction = true;


			}
			else if(myType == EnemyType.QUEEN)
			{
				List<EnemyStatusScript> enemy = new List<EnemyStatusScript>();

				for(int i = 0; i < BattleManagerScript.Instance.enemyList.Count; i++)
				{
					if(BattleManagerScript.Instance.enemyList[i] != null)
					{
						if(BattleManagerScript.Instance.enemyList[i].health < (50 / 100 * (BattleManagerScript.Instance.enemyList[i].maxHealth)))
						{
							enemy.Add(BattleManagerScript.Instance.enemyList[i]);
						}
					}
				}

				bool kingless = true;
				int heal = Random.Range(attack, (int)(attack * 1.5));

				if(enemy.Count > 2)
				{
					HealTeam(heal + 2);
				}
				else if(enemy.Count > 0)
				{
					for(int i = 0; i < enemy.Count; i++)
					{
						if(enemy[i] != null)
						{
							if(enemy[i].myType == EnemyType.KING)
							{
								kingless = false;
								Heal(enemy[i], heal);
							}
						}
					}

					if(kingless)
					{
						int rand = Random.Range(0, enemy.Count);

						Heal(enemy[rand], heal);
					}
				}
				else
				{
					int rand = Random.Range(1, attack + 1);

					Attack(rand);
				}

				yield return new WaitForSeconds(2f);

				BattleManagerScript.Instance.enemyAction = true;

			}
			else if(myType == EnemyType.KING)
			{
				if(health < (40/100 * maxHealth))
				{
					bool knightless = true;

					if(BattleManagerScript.Instance.enemyList.Count < 5)
					{
						for(int i = 0; i < BattleManagerScript.Instance.enemyList.Count; i++)
						{
							if(BattleManagerScript.Instance.enemyList[i].myType == EnemyType.KNIGHT)
							{
								knightless = false;
							}
						}
					}

					if(knightless)
					{
						int rand = Random.Range(0, 4);

						if(rand > 2)
						{
							GameObject newSpawn = Resources.Load("Prefabs/Enemies/Knight") as GameObject;
							EnemyStatusScript spawnScript = newSpawn.GetComponent<EnemyStatusScript>();
							BattleManagerScript.Instance.enemyList.Add(spawnScript);
							BattleManagerScript.Instance.enemyAction = true;
						}
						else if(rand > 0)
						{
							GameObject newSpawn = Resources.Load("Prefabs/Enemies/Normal") as GameObject;
							EnemyStatusScript spawnScript = newSpawn.GetComponent<EnemyStatusScript>();
							BattleManagerScript.Instance.enemyList.Add(spawnScript);
							BattleManagerScript.Instance.enemyAction = true;
						}
						else
						{
							rand = Random.Range(attack, (int)(attack * 1.5));

							HeavyAttack(rand);
						}
					}
				}
				else
				{
					int rand = Random.Range(1, attack + 1);

					Attack(rand);
				}

				yield return new WaitForSeconds(2f);

				BattleManagerScript.Instance.enemyAction = true;
			}
		}
	}

	public void Heal(EnemyStatusScript healee, int regen)
	{
		healee.health += regen;

		if(healee.health > healee.maxHealth)
		{
			healee.health = healee.maxHealth;
		}

	}

	public void HealTeam(int heal)
	{
		for(int i = 0; i < BattleManagerScript.Instance.enemyList.Count; i++)
		{
			if(BattleManagerScript.Instance.enemyList[i] != null)
			{
				BattleManagerScript.Instance.enemyList[i].health += heal;

				if(BattleManagerScript.Instance.enemyList[i].health > BattleManagerScript.Instance.enemyList[i].maxHealth)
				{
					BattleManagerScript.Instance.enemyList[i].health = BattleManagerScript.Instance.enemyList[i].maxHealth;
				}
			}
		}
	}
}
