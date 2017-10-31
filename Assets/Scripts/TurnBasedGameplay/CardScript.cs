using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum CardType
{
	ATTACK,
	HEAL,
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

	public Text myDmg;
	public Text myManaCost;
    public Text myDescription;
	public Image myImage;
	public EnemyStatusScript target;

	public ParticleSystem redTargeted;
	public ParticleSystem blueNotTargeted;

	// Use this for initialization
	void Start () 
	{
		redTargeted = gameObject.GetComponent<ParticleSystem>();
		blueNotTargeted = gameObject.GetComponent<ParticleSystem>();

		ActivateBlueNotTargeted();
		//myImage = this.GetComponent<Image>();
		//interactable = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void ActivateRedTargeted()
	{
		if(target)
		{
			if(redTargeted.isStopped)
			{
				redTargeted.Play();
			}
		}
	}

	void DeactivateRedTargeted()
	{
		if(!target)
		{
			if(redTargeted.isPlaying)
			{
				redTargeted.Stop();
			}
		}
	}

	void ActivateBlueNotTargeted()
	{
		if(!target)
		{
			if(blueNotTargeted.isStopped)
			{
				blueNotTargeted.Play();
			}
		}
	}

	void DeactivateBlueNotTargeted()
	{
		if(target)
		{
			if(blueNotTargeted.isPlaying)
			{
				blueNotTargeted.Stop();
			}
		}
	}

	public void HideSelf()
	{
		myImage.enabled = false;
		interactable = false;
		myDmg.enabled = false;
		myManaCost.enabled = false;
		myDescription.enabled = false;
	}

	public void UpdateStats()
	{
		myDmg.enabled = true;
		myManaCost.enabled = true;
		myDescription.enabled = true;
		myDmg.text = myCard.cardEffect.ToString();
		myManaCost.text = myCard.manaCost.ToString();
        myDescription.text = myCard.description.ToString();
		myImage.enabled = true;
		myImage.sprite = myCard.cardImage;
		interactable = true;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Tap");

		if(interactable)
		{
			if(BattleManagerScript.Instance.currTurn == BattleStates.CHOOSE_CARDS)
			{
				if(!selected)
				{
					if(cardPouch.manaCheck - myCard.manaCost >= 0)
					{
						cardPouch.manaCheck -= myCard.manaCost;
						cardPouch.selectedCards.Add(this);
						selected = true;
						GetComponent<Image>().color = Color.blue;
					}
				}
				else
				{
					cardPouch.manaCheck += myCard.manaCost;
					cardPouch.selectedCards.Remove(this);
					selected = false;
					GetComponent<Image>().color = Color.white;
				}
			}
			else if(BattleManagerScript.Instance.currTurn == BattleStates.CHOOSE_ENEMIES)
			{
				if(myCard.cardType == CardType.ATTACK)
				{
					if(!selected)
					{
						if(BattleManagerScript.Instance.selectedCard != null)
						{
							if(BattleManagerScript.Instance.selectedCard.target == null)
							{
								BattleManagerScript.Instance.selectedCard.myImage.color = Color.blue;
								DeactivateBlueNotTargeted();
								ActivateRedTargeted();
							}

							BattleManagerScript.Instance.selectedCard.selected = false;
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
