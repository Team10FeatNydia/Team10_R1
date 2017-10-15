using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardPouchScript : MonoBehaviour, IPointerClickHandler
{

    BattleManagerScript battleManager;
    public GameObject[] displayedCards = new GameObject[5];
    public GameObject[] displayedSpells = new GameObject[2];
    public List<CardScript> selectedCards = new List<CardScript>();
    public List<SpellsScript> selectedSpells = new List<SpellsScript>();
    public Sprite cardEye;
    public Sprite spellsEye;
    public Sprite defaultEye;
    public Vector2 fingerStartPos = Vector2.zero;
    public float minSwipeDist = 10.0f;
    public bool isSwipe = false;
    public bool opened = false;
    public bool cardAction;
    public bool spellsAction;
    public int manaCheck;

    private int spellsAttack = 0;
    private int spellsHeal = 0;
    private bool spellsAttackbool = false;
    private bool spellsHealbool = false;
    private float touch1;
    private Sprite currentEye;

    // Use this for initialization
    void Start()
    {
        battleManager = BattleManagerScript.Instance;
        GetComponent<Image>().sprite = defaultEye;
    }

    // Update is called once per frame
    void Update()
    {


        Swipe();
        if (BattleManagerScript.Instance.currTurn == BattleStates.ENEMY_TURN)
        {
            spellsHealbool = false;
            spellsAttackbool = false;
        }
    }

    void Attack()
    {
        for (int i = 0; i < selectedCards.Count; i++)
        {
            if (spellsAttackbool == true)
            {
                spellsAttack = 1;
                Debug.Log("spellsattack");
                Debug.Log(spellsAttack);
            }
            else if (spellsHealbool == true)
            {
                spellsHeal = 1;
                Debug.Log("spellsheal");
                Debug.Log(spellsHeal);
            }

            if (selectedCards[i].myCard.cardType == CardType.ATTACK)
            {
                battleManager.target.health -= selectedCards[i].myCard.cardEffect + spellsAttack;
				battleManager.player.localPlayerData.health += spellsHeal;

            }

            else if (selectedCards[i].myCard.cardType == CardType.HEAL)
            {
				battleManager.player.localPlayerData.health += selectedCards[i].myCard.cardEffect + spellsHeal;
                battleManager.target.health -= spellsAttack;
            }

			battleManager.player.localPlayerData.manaPoints -= selectedCards[i].myCard.manaCost;
        }

        Debug.Log("Attack");

        //battleManager.target.health -= battleManager.player.attack;
        battleManager.target.GetComponent<MeshRenderer>().material.color = Color.red;

        battleManager.target.CheckHealth();

        battleManager.target = null;
        battleManager.currTurn = BattleStates.ENEMY_TURN;

        for (int i = 0; i < displayedCards.Length; i++)
        {
            Destroy(displayedCards[i].gameObject);
        }
    }

    void Spells()
    {
        for (int i = 0; i < selectedSpells.Count; i++)
        {
            if (selectedSpells[i].mySpells.spellsType == SpellsType.ATTACK_SPELL)
            {
                spellsHealbool = false;
                spellsAttackbool = true;
            }
            else if (selectedSpells[i].mySpells.spellsType == SpellsType.HEAL_SPELL)
            {
                spellsHealbool = true;
                spellsAttackbool = false;
            }
			battleManager.player.localPlayerData.manaPoints -= selectedSpells[i].mySpells.manaCost;
        }

        for (int i = 0; i < displayedSpells.Length; i++)
        {
            Destroy(displayedSpells[i].gameObject);
        }

       
    }

    void LayOutCards()
    {
		manaCheck = battleManager.player.localPlayerData.manaPoints;
        selectedCards.Clear();

        for (int i = 0; i < CardManagerScript.Instance.cardList.Count; i++)
        {
            CardDescription tempCard;
            tempCard = CardManagerScript.Instance.cardList[i];
            tempCard.isSpawned = false;
            CardManagerScript.Instance.cardList[i] = tempCard;
        }

        for (int i = 0; i < 5; i++)
        {
            bool exitLoop = false;
            CardDescription tempCard;

            int randNum;

            do
            {
                randNum = Random.Range(0, CardManagerScript.Instance.cardList.Count);

                if (!CardManagerScript.Instance.cardList[randNum].isSpawned)
                {
                    tempCard = CardManagerScript.Instance.cardList[randNum];
                    tempCard.isSpawned = true;
                    CardManagerScript.Instance.cardList[randNum] = tempCard;
                    exitLoop = true;
                }
            } while (!exitLoop);

            GameObject newCard = Instantiate(CardManagerScript.Instance.cardPrefab, this.transform) as GameObject;

            CardScript cardScript = newCard.GetComponent<CardScript>();
            cardScript.myCard = CardManagerScript.Instance.cardList[randNum];
            cardScript.cardPouch = this;

            newCard.GetComponent<RectTransform>().localPosition = new Vector3(-135f * i - 150f, 0f, 0f);
            newCard.transform.SetParent(this.transform.parent, true);

            displayedCards[i] = newCard;
            displayedCards[i].GetComponent<CardScript>().UpdateStats();
        }
    }

    void LayoutSpells()
    {
		manaCheck = battleManager.player.localPlayerData.manaPoints;
        selectedSpells.Clear();

        for (int i = 0; i < SpellsManagerScript.Instance.spellsList.Count; i++)
        {
            SpellsDescription tempSpells;
            tempSpells = SpellsManagerScript.Instance.spellsList[i];
            tempSpells.isSpawned = false;
            SpellsManagerScript.Instance.spellsList[i] = tempSpells;
        }

        for (int i = 0; i < SpellsManagerScript.Instance.spellsList.Count; i++)
        {
            bool exitLoop = false;
            SpellsDescription tempSpells;

            do
            {
                if (!SpellsManagerScript.Instance.spellsList[i].isSpawned)
                {
                    tempSpells = SpellsManagerScript.Instance.spellsList[i];
                    tempSpells.isSpawned = true;
                    SpellsManagerScript.Instance.spellsList[i] = tempSpells;
                    exitLoop = true;
                }
            } while (!exitLoop);

            GameObject newSpells = Instantiate(SpellsManagerScript.Instance.spellsPrefab, this.transform) as GameObject;

            SpellsScript spellsScript = newSpells.GetComponent<SpellsScript>();
            spellsScript.mySpells = SpellsManagerScript.Instance.spellsList[i];
            spellsScript.cardPouch = this;

            newSpells.GetComponent<RectTransform>().localPosition = new Vector3(-135f * i - 150f, 0f, 0f);
            newSpells.transform.SetParent(this.transform.parent, true);

            displayedSpells[i] = newSpells;
            displayedSpells[i].GetComponent<SpellsScript>().UpdateStats();
        }

    }

    void Swipe()
    {

        if (Input.touchCount > 0)
        {

            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        isSwipe = true;
                        fingerStartPos = touch.position;
                        touch1 = touch.position.y;
                        break;

                    case TouchPhase.Canceled:
                        isSwipe = false;
                        break;

                    case TouchPhase.Ended:

                        float swipeDist = (touch.position - fingerStartPos).magnitude;
                        Debug.Log("swipedist    " + swipeDist);
                        Debug.Log("touch1   " + touch1);

                        if (touch1 < 250)
                        {

                            if (isSwipe && swipeDist > minSwipeDist)
                            {
                                Vector2 direction = touch.position - fingerStartPos;
                                Vector2 swipeType = Vector2.zero;


                                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                                {
                                    swipeType = Vector2.right * Mathf.Sign(direction.x);
                                }
                                else
                                {
                                    swipeType = Vector2.up * Mathf.Sign(direction.y);
                                }

                                if (swipeType.x != 0.0f)
                                {
                                    Debug.Log("swiped");
                                    GetComponent<Image>().sprite = spellsEye;
                                    cardAction = false;
                                    spellsAction = true;
                                }

                                if (swipeType.y != 0.0f)
                                {
                                    Debug.Log("swiped");
                                    GetComponent<Image>().sprite = cardEye;
                                    cardAction = true;
                                    spellsAction = false;
                                }

                            }
                        }

                        break;
                }
            }
        }

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");

        if (opened && cardAction)
        {
            if (battleManager.target != null && selectedCards.Count > 0)
            {
                Attack();
                opened = false;

            }
        }

        else if (opened && spellsAction)
        {
            if (selectedSpells.Count == 1)
            {
                Spells();
                opened = false;

            }
        }
        else if (cardAction)
        {
            opened = true;
            LayOutCards();
            GetComponent<Image>().sprite = cardEye;

        }

        else if (spellsAction)
        {
            opened = true;
            LayoutSpells();
            GetComponent<Image>().sprite = spellsEye;
        }

    }
}
