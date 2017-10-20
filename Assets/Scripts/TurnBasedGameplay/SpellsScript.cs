using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum SpellsType
{
    ATTACK_SPELL,
    HEAL_SPELL,
    TOTAL,
}


[System.Serializable]
public struct SpellsDescription
{
    public SpellsType spellsType;
    //public int spellsUsageAmount;
    public int manaCost;
    public string description;
    public Sprite spellsImage;
    public bool isSpawned;
}

public class SpellsScript : MonoBehaviour, IPointerClickHandler
{
    public SpellsDescription mySpells;
    public CardPouchScript cardPouch;
    public bool selected;
	public bool interactable = false;
    public Text myManaCost;
    public Text myDescription;
	public Image myImage;

	void Start()
	{
		//myImage = this.GetComponent<Image>();
		//interactable = false;
	}

    void Update()
    {
        if (selected)
        {
            GetComponent<Image>().color = Color.blue;
        }

        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }

	public void HideSelf()
	{
		myImage.enabled = false;
		interactable = false;
		myManaCost.enabled = false;
		myDescription.enabled = false;
	}

    public void UpdateStats()
    {
		myManaCost.enabled = true;
		myDescription.enabled = true;
        myManaCost.text = "Mana Cost: " + mySpells.manaCost.ToString();
        myDescription.text =  mySpells.description.ToString();
		myImage.enabled = true;
		myImage.sprite = mySpells.spellsImage;
		interactable = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Tap");

        if (!selected)
        {
            if (cardPouch.manaCheck - mySpells.manaCost >= 0)
            {
                cardPouch.manaCheck -= mySpells.manaCost;
                cardPouch.selectedSpells.Add(this);
                selected = true;
            }
        }
        else
        {
            cardPouch.manaCheck += mySpells.manaCost;
            cardPouch.selectedSpells.Remove(this);
            selected = false;
        }
    }
}
