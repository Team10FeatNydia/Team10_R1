﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum CardType
{
	ATTACK,
	HEAL,
	STUN,
	TOTAL,
}

[System.Serializable]
public struct CardDescription
{
	public CardType cardType;
	public int cardEffect;
	public int manaCost;
    public string description;
	public Sprite cardImage;
	public bool isSpawned;
}

public class CardScript : MonoBehaviour, IPointerClickHandler 
{
	public CardDescription myCard;
	public CardPouchScript cardPouch;
	public bool selected;
	public bool interactable = false;
	public RectTransform rectTransform;
	float rotate = 1f;
	bool canShake = false;
	public float shakeTime = 20.0f;
	public Vector3 origin;

	public Text myDmg;
	public Text myManaCost;
    public Text myDescription;
	public Image myImage;
	public EnemyStatusScript target;

	//LerpingCards
	public bool canMoveLeft;
	public bool canMoveRight;
	public Vector3 startPos;
	public Vector3 endPos;
	float distance;

	float lerpTime = 0.5f;
	float currLerpTime = 0f;
	public Vector3 hopStartPoint;
	public Vector3 hopEndPoint;
	public float hopTime;
	public float hopHeight;

	// Use this for initialization
	void Start () 
	{
		rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(canMoveLeft)
		{
			float percentage = currLerpTime / lerpTime;

			transform.position = Vector3.Lerp(startPos, endPos, percentage);

			currLerpTime += Time.deltaTime;

			if(currLerpTime >= lerpTime)
			{
				currLerpTime = lerpTime;
				canMoveLeft = false;
			}
		}

		if(canMoveRight)
		{
			float percentage = currLerpTime / lerpTime;

			transform.position = Vector3.Lerp(startPos, endPos, percentage);

			currLerpTime += Time.deltaTime;

			if(currLerpTime >= lerpTime)
			{
				currLerpTime = lerpTime;
				canMoveRight = false;
			}
		}

		if(interactable)
		{
			if(!canShake)
			{
				shakeTime -= Time.deltaTime * 10f;
				if(shakeTime <= 0f) canShake = true;
			}
			else
			{
				Quaternion shakePos = rectTransform.rotation;
				if(shakePos.z > 0.05f) 
				{
					rotate = -1f;
				}
				else if (shakePos.z < -0.05f) 
				{
					canShake = false;
					rotate = 1f;
					shakeTime = 20.0f;
					rectTransform.rotation = Quaternion.identity;
					return;

				}
				rectTransform.Rotate(new Vector3 (0, 0, rotate));
			}
		}
	}

	public void Attack()
	{
		if (myCard.cardType == CardType.ATTACK)
		{
			bool kingless = true;

			if(target.myType == EnemyType.KING)
			{
				kingless = false;
			}

			if(!kingless)
			{
				for(int j = 0; j < BattleManagerScript.Instance.enemyList.Count; j++)
				{
					if(BattleManagerScript.Instance.enemyList[j] != null)
					{
						if(BattleManagerScript.Instance.enemyList[j].myType == EnemyType.KNIGHT)
						{
							target = BattleManagerScript.Instance.enemyList[j];
						}
					}
				}
			}

			target.getAttacked.Play();
			// Attacking Script
			SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_ATTACK2);
			target.health -= myCard.cardEffect;

			target.CheckHealth();

		}
		else if (myCard.cardType == CardType.HEAL)
		{
			BattleManagerScript.Instance.player.healingPlayer.Play();

            PlayerStatSaver.mInstance.playerHP += myCard.cardEffect;
			//BattleManagerScript.Instance.player.localPlayerData.health += myCard.cardEffect;
			if(BattleManagerScript.Instance.player.localPlayerData.health > BattleManagerScript.Instance.player.localPlayerData.maxHealth)
			{
				BattleManagerScript.Instance.player.localPlayerData.health = BattleManagerScript.Instance.player.localPlayerData.maxHealth;
			}
		}
		else if(myCard.cardType == CardType.STUN)
		{
			bool kingless = true;

			if(target.myType == EnemyType.KING)
			{
				kingless = false;
			}

			if(!kingless)
			{
				for(int j = 0; j < BattleManagerScript.Instance.enemyList.Count; j++)
				{
					if(BattleManagerScript.Instance.enemyList[j] != null)
					{
						if(BattleManagerScript.Instance.enemyList[j].myType == EnemyType.KNIGHT)
						{
							target = BattleManagerScript.Instance.enemyList[j];
						}
					}
				}
			}

			if(!target.stunned) target.stunned = true;
		}
	}

	public void StartHop(Vector3 endPoint, float time, float hopHeight, float waitTime)
	{
		StartCoroutine(Hop(endPoint, time, hopHeight, waitTime));
	}

	IEnumerator Hop(Vector3 endPoint, float time, float hopHeight, float waitTime)
	{
		yield return new WaitForSeconds(waitTime);

		Attack();

		yield return new WaitForSeconds(0.1f);

		Vector3 startPos = transform.position;
		float timer = 0.0f;

		while(timer <= 1.0f)
		{
			float height = Mathf.Sin(Mathf.PI * timer) * hopHeight;
			transform.position = Vector3.Lerp(startPos, endPoint, timer) + Vector3.up * height;

			timer += Time.deltaTime / time;
			yield return null;
		}
	}

	public void SetLerpPos(Vector3 end, bool moveLeft, bool moveRight)
	{
		currLerpTime = 0;

		startPos = transform.position;
		endPos = end;

		canMoveLeft = moveLeft;
		canMoveRight = moveRight;
	}

	public void HideSelf()
	{
		myImage.enabled = false;
		interactable = false;
		myDmg.enabled = false;
		myManaCost.enabled = false;
	}

	public void UpdateStats()
	{
		myDmg.enabled = true;
		myManaCost.enabled = true;
		myDmg.text = myCard.cardEffect.ToString();
		myManaCost.text = myCard.manaCost.ToString();
		myImage.enabled = true;
		myImage.sprite = myCard.cardImage;
		interactable = true;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if(TutorialManagerScript.mInstance.tutorialOpened)
		{
			return;
		}

		if(interactable)
		{
			if(BattleManagerScript.Instance.currTurn == BattleStates.CHOOSE_CARDS)
			{
				if(!selected)
				{
					if(cardPouch.manaCheck - myCard.manaCost >= 0)
					{
						cardPouch.manaCheck -= myCard.manaCost;
						BattleManagerScript.Instance.manaText.text = cardPouch.manaCheck.ToString();
						cardPouch.selectedCards.Add(this);
						selected = true;
						GetComponent<Image>().color = Color.blue;
						rectTransform.localPosition += new Vector3(0f, 30f, 0f);
					}
				}
				else
				{
					cardPouch.manaCheck += myCard.manaCost;
					BattleManagerScript.Instance.manaText.text = cardPouch.manaCheck.ToString();
					cardPouch.selectedCards.Remove(this);
					selected = false;
					GetComponent<Image>().color = Color.white;
					rectTransform.localPosition -= new Vector3(0f, 30f, 0f);
				}
			}
			else if(BattleManagerScript.Instance.currTurn == BattleStates.CHOOSE_ENEMIES)
			{
				if(myCard.cardType == CardType.ATTACK || myCard.cardType == CardType.STUN)
				{
					if(!selected)
					{
						if(BattleManagerScript.Instance.selectedCard != null)
						{
							if(BattleManagerScript.Instance.selectedCard.target == null)
							{
								BattleManagerScript.Instance.selectedCard.myImage.color = Color.blue;
							}
							else
							{
								BattleManagerScript.Instance.selectedCard.myImage.color = Color.white;
							}

							BattleManagerScript.Instance.selectedCard.selected = false;
						}

						for(int i = 0; i < BattleManagerScript.Instance.enemyList.Count; i++)
						{
							if(BattleManagerScript.Instance.enemyList[i] != null)
							{
								if(BattleManagerScript.Instance.enemyList[i].interactable)
								{
									BattleManagerScript.Instance.enemyList[i].redTarget.Stop();
									BattleManagerScript.Instance.enemyList[i].blueTarget.Play();
								}
								else
								{
									BattleManagerScript.Instance.enemyList[i].redTarget.Stop();
									BattleManagerScript.Instance.enemyList[i].blueTarget.Stop();
								}
							}
						}

						if(target != null)
						{
							target.redTarget.Play();
						}

						BattleManagerScript.Instance.selectedCard = this;
						selected = true;
						myImage.color = Color.red;
					}
				}
			}
		}
	}
}
