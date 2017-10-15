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
    public Text myManaCost;
    public Text myDescription;


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
        public void UpdateStats()
        {
            myManaCost.text = "Mana Cost: " + mySpells.manaCost.ToString();
            myDescription.text =  mySpells.description.ToString();
            GetComponent<Image>().sprite = mySpells.spellsImage;
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
