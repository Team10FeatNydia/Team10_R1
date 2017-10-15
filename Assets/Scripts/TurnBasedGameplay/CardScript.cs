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

public class CardScript : MonoBehaviour, IPointerClickHandler {

	public CardDescription myCard;
	public CardPouchScript cardPouch;
	public bool selected;

	public Text myDmg;
	public Text myManaCost;
    public Text myDescription;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (selected)
        {
            GetComponent<Image>().color = Color.blue;
        }

        else
        {
            GetComponent<Image>().color = Color.white;
        }
	}

	public void UpdateStats()
	{
		myDmg.text = myCard.cardEffect.ToString();
		myManaCost.text = myCard.manaCost.ToString();
        myDescription.text = myCard.description.ToString();
		GetComponent<Image>().sprite = myCard.cardImage;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Tap");

		if(!selected)
		{
			if(cardPouch.manaCheck - myCard.manaCost >= 0)
			{
				cardPouch.manaCheck -= myCard.manaCost;
				cardPouch.selectedCards.Add(this);
				selected = true;
			}
		}
		else
		{
			cardPouch.manaCheck += myCard.manaCost;
			cardPouch.selectedCards.Remove(this);
			selected = false;
		}
	}
}
