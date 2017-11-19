using System.Collections;
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

	// Use this for initialization
	void Start () 
	{
		rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () 
	{
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
					//Debug.Log("Front");
				}
				else if (shakePos.z < -0.05f) 
				{
					canShake = false;
					rotate = 1f;
					shakeTime = 20.0f;
					rectTransform.rotation = Quaternion.identity;
					return;
					////Debug.Log("Back");

				}
				//rectTransform.rotation = Quaternion.Euler(0f, 0f, shakePos.z);
				rectTransform.Rotate(new Vector3 (0, 0, rotate));
				//Debug.Log(shakePos.z);
			}
		}
	}

	public void HideSelf()
	{
		myImage.enabled = false;
		interactable = false;
//		myDmg.enabled = false;
//		myManaCost.enabled = false;
//		myDescription.enabled = false;
	}

	public void UpdateStats()
	{
//		myDmg.enabled = true;
//		myManaCost.enabled = true;
//		myDescription.enabled = true;
//		myDmg.text = myCard.cardEffect.ToString();
//		myManaCost.text = myCard.manaCost.ToString();
//      myDescription.text = myCard.description.ToString();
		myImage.enabled = true;
		myImage.sprite = myCard.cardImage;
		interactable = true;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		//Debug.Log("Tap");

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
						//Debug.Log(cardPouch.manaCheck);
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
					//Debug.Log(cardPouch.manaCheck);
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
								if(interactable)
								{
									BattleManagerScript.Instance.enemyList[i].redTarget.Stop();
									BattleManagerScript.Instance.enemyList[i].blueTarget.Play();
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

	public void SwitchParents()
	{
		if(transform.parent.name.Contains("Hand"))
		{
			transform.SetParent(CardManagerScript.Instance.cardDeckPanel);
			//transform.parent = CardManagerScript.Instance.cardHandPanel.transform;
			CardManagerScript.Instance.cardList.Add(myCard);
			CardManagerScript.Instance.handList.Remove(myCard);
		}
		else if(transform.parent.name.Contains("Deck"))
		{
			transform.SetParent(CardManagerScript.Instance.cardHandPanel);
			//transform.parent = CardManagerScript.Instance.cardDeckPanel.transform;
			CardManagerScript.Instance.handList.Add(myCard);
			CardManagerScript.Instance.cardList.Remove(myCard);
		}
	}
}
