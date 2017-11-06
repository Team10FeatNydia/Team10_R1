using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum PouchStates
{
	ACTION_CARDS,
	SPELL_CARDS,
}

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
	public Animator UIBorderBar;

	bool cardsLayedout = false;
	bool spellsLayedout = false;
	PouchStates currState;
    private int spellsAttack = 0;
    //private int spellsHeal = 0;
    private bool spellsAttackbool = false;
    private bool spellsHealbool = false;
    private float touch1;
    private Sprite currentEye;

    // Use this for initialization
    void Start()
    {
        battleManager = BattleManagerScript.Instance;
        GetComponent<Image>().sprite = defaultEye;
//		UIBorderBar.SetBool("PlaySlideIn", false);
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
		int spellsHeal = 0;
		int spellsDmg = 0;

		for(int i = 0; i < selectedSpells.Count; i++)
		{
			if(selectedSpells[i].mySpells.spellsType == SpellsType.HEAL_SPELL)
			{
				//spellsHeal = selectedSpells[i].mySpells.
				spellsHeal += 1;
			}
			else if(selectedSpells[i].mySpells.spellsType == SpellsType.HEAL_SPELL)
			{
				//spellsHeal = selectedSpells[i].mySpells.
				spellsDmg += 1;
			}
		}

        for (int i = 0; i < selectedCards.Count; i++)
        {
//            if (spellsAttackbool == true)
//            {
//                spellsAttack = 1;
//                Debug.Log("spellsattack");
//                Debug.Log(spellsAttack);
//            }
//            else if (spellsHealbool == true)
//            {
//                spellsHeal = 1;
//                Debug.Log("spellsheal");
//                Debug.Log(spellsHeal);
//            }

            if (selectedCards[i].myCard.cardType == CardType.ATTACK)
            {
				bool kingless = true;

				if(selectedCards[i].target.myType == EnemyType.KING)
				{
					kingless = false;
				}

				if(!kingless)
				{
					for(int j = 0; j < battleManager.enemyList.Count; j++)
					{
						if(battleManager.enemyList[j] != null)
						{
							if(battleManager.enemyList[j].myType == EnemyType.KNIGHT)
							{
								selectedCards[i].target = battleManager.enemyList[j];
							}
						}
					}
				}


				selectedCards[i].target.swordAttackPlayer.Play();
				selectedCards[i].target.health -= selectedCards[i].myCard.cardEffect + spellsDmg;
				battleManager.player.localPlayerData.health += spellsHeal;

            }
            else if (selectedCards[i].myCard.cardType == CardType.HEAL)
            {
				battleManager.player.localPlayerData.health += selectedCards[i].myCard.cardEffect + spellsHeal;
				for(int j = 0; j < battleManager.enemyList.Count; j++)
				{
					battleManager.enemyList[j].health -= spellsDmg;
				}
            }
			else if(selectedCards[i].myCard.cardType == CardType.STUN)
			{
				
				bool kingless = true;

				if(selectedCards[i].target.myType == EnemyType.KING)
				{
					kingless = false;
				}

				if(!kingless)
				{
					for(int j = 0; j < battleManager.enemyList.Count; j++)
					{
						if(battleManager.enemyList[j] != null)
						{
							if(battleManager.enemyList[j].myType == EnemyType.KNIGHT)
							{
								selectedCards[i].target = battleManager.enemyList[j];
							}
						}
					}
				}

				if(!selectedCards[i].target.stunned) selectedCards[i].target.stunned = true;
			}

			battleManager.player.localPlayerData.manaPoints -= selectedCards[i].myCard.manaCost;
        }

        Debug.Log("Attack");

        //battleManager.target.health -= battleManager.player.attack;
        //battleManager.target.GetComponent<MeshRenderer>().material.color = Color.red;

		for(int i = 0; i < selectedCards.Count; i++)
		{
			if(selectedCards[i].target != null)
			{
				selectedCards[i].target.CheckHealth();
			}
		}

		for(int i = 0; i < battleManager.enemyList.Count; i++)
		{
			battleManager.enemyList[i].blueTarget.Stop();
			battleManager.enemyList[i].redTarget.Stop();

		}

        //battleManager.target.CheckHealth();

        //battleManager.target = null;
        battleManager.currTurn = BattleStates.ENEMY_TURN;
		GetComponent<Image>().sprite = defaultEye;

		DestroyCards();
    }

//    void Spells()
//    {
//        for (int i = 0; i < selectedSpells.Count; i++)
//        {
//            if (selectedSpells[i].mySpells.spellsType == SpellsType.ATTACK_SPELL)
//            {
//                spellsHealbool = false;
//                spellsAttackbool = true;
//            }
//            else if (selectedSpells[i].mySpells.spellsType == SpellsType.HEAL_SPELL)
//            {
//                spellsHealbool = true;
//                spellsAttackbool = false;
//            }
//			battleManager.player.localPlayerData.manaPoints -= selectedSpells[i].mySpells.manaCost;
//        }
//
//        for (int i = 0; i < displayedSpells.Length; i++)
//        {
//            Destroy(displayedSpells[i].gameObject);
//        }
//
//       
//    }

    void LayOutCards()
    {
		manaCheck = battleManager.player.localPlayerData.manaPoints;
        selectedCards.Clear();

		for(int i = 0; i < displayedSpells.Length; i++)
		{
			if(displayedSpells[i] != null) displayedSpells[i].GetComponent<SpellsScript>().HideSelf();
		}

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

            newCard.GetComponent<RectTransform>().localPosition = new Vector3(-180f * i - 220f, 0f, 0f);
            newCard.transform.SetParent(this.transform.parent, true);

            displayedCards[i] = newCard;
            displayedCards[i].GetComponent<CardScript>().UpdateStats();

			cardsLayedout = true;
        }
    }

    void LayoutSpells()
    {
		manaCheck = battleManager.player.localPlayerData.manaPoints;
        selectedSpells.Clear();

		for(int i = 0; i < displayedCards.Length; i++)
		{
			if(displayedCards[i] != null) displayedCards[i].GetComponent<CardScript>().HideSelf();
		}

        for (int i = 0; i < SpellsManagerScript.Instance.spellsList.Count; i++)
        {
            SpellsDescription tempSpells;
            tempSpells = SpellsManagerScript.Instance.spellsList[i];
            tempSpells.isSpawned = false;
            SpellsManagerScript.Instance.spellsList[i] = tempSpells;
        }

		for(int i = 0; i < 5; i++)
		{
			if(i < SpellsManagerScript.Instance.spellsList.Count)
			{
				bool exitLoop = false;
				SpellsDescription tempSpells;

				int randNum;

				do
				{
					randNum = Random.Range(0, SpellsManagerScript.Instance.spellsList.Count);

					if (!SpellsManagerScript.Instance.spellsList[randNum].isSpawned)
					{
						tempSpells = SpellsManagerScript.Instance.spellsList[randNum];
						tempSpells.isSpawned = true;
						SpellsManagerScript.Instance.spellsList[randNum] = tempSpells;
						exitLoop = true;
					}
				} while (!exitLoop);

				GameObject newSpells = Instantiate(SpellsManagerScript.Instance.spellsPrefab, this.transform) as GameObject;

				SpellsScript spellsScript = newSpells.GetComponent<SpellsScript>();
				spellsScript.mySpells = SpellsManagerScript.Instance.spellsList[randNum];
				spellsScript.cardPouch = this;

				newSpells.GetComponent<RectTransform>().localPosition = new Vector3(-180f * i - 220f, 0f, 0f);
				newSpells.transform.SetParent(this.transform.parent, true);

				displayedSpells[i] = newSpells;
				displayedSpells[i].GetComponent<SpellsScript>().UpdateStats();
			}
		}

		spellsLayedout = true;

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
									if(direction.x > 0)
									{
										DestroyCards();
										battleManager.currTurn = BattleStates.PLAYER_TURN;
										GetComponent<Image>().sprite = defaultEye;

										UIBorderBar.Play("BorderSlideOut");
									}
									else
									{
										if(battleManager.currTurn == BattleStates.CHOOSE_CARDS)
										{
											if(currState == PouchStates.ACTION_CARDS)
											{
												Debug.Log("swiped");
												GetComponent<Image>().sprite = spellsEye;

												for(int i = 0; i < displayedCards.Length; i++)
												{
													displayedCards[i].GetComponent<CardScript>().HideSelf();
												}	

												if(!spellsLayedout) 
												{
													LayoutSpells();
												}
												else 
												{
													for(int i = 0; i < displayedSpells.Length; i++)
													{
														displayedSpells[i].GetComponent<SpellsScript>().UpdateStats();
													}
												}
												cardAction = false;
												spellsAction = true;
												currState = PouchStates.SPELL_CARDS;
											}
											else if(currState == PouchStates.SPELL_CARDS)
											{
												Debug.Log("swiped");
												GetComponent<Image>().sprite = cardEye;
												
												for(int i = 0; i < displayedSpells.Length; i++)
												{
													displayedSpells[i].GetComponent<SpellsScript>().HideSelf();
												}	

												if(!cardsLayedout) 
												{
													LayOutCards();
												}
												else 
												{
													for(int i = 0; i < displayedCards.Length; i++)
													{
														displayedCards[i].GetComponent<CardScript>().UpdateStats();
													}
												}
												cardAction = true;
												spellsAction = false;
												currState = PouchStates.ACTION_CARDS;
											}
										}

									}
                                }

//                                if (swipeType.y != 0.0f)
//                                {
//									if(direction.x > 0)
//									{
//										DestroyCards();
//										battleManager.currTurn = BattleStates.PLAYER_TURN;
//										GetComponent<Image>().sprite = defaultEye;
//									}
//									else
//									{
//										if(battleManager.currTurn == BattleStates.CHOOSE_CARDS)
//										{
//											if(currState == PouchStates.SPELL_CARDS)
//											{
//												
//											}
//										}
//									}
//									
//                                }

                            }
                        }

                        break;
                }
            }
        }

    }

	public void DisplaySelectedCards()
	{
		for(int i = 0; i < displayedCards.Length; i++)
		{
			if(!displayedCards[i].GetComponent<CardScript>().selected)
			{
				Destroy(displayedCards[i]);
			}
		}

		for(int i = 0; i < selectedCards.Count; i++)
		{
			if(selectedCards[i].myCard.cardType != CardType.ATTACK)
			{
				selectedCards[i].GetComponent<Image>().color = Color.white;
			}

			selectedCards[i].GetComponent<RectTransform>().localPosition = new Vector3(-180f * i - 220f, 0f, 0f);
			selectedCards[i].UpdateStats();
			selectedCards[i].selected = false;
		}
	}

	public void DestroyCards()
	{
		for(int i = 0; i < displayedCards.Length; i++)
		{
			if(displayedCards[i] != null)
			{
				Destroy(displayedCards[i]);
			}
		}

		for(int i = 0; i < displayedSpells.Length; i++)
		{
			if(displayedSpells[i] != null)
			{
				Destroy(displayedSpells[i]);
			}
		}

		for(int i = 0; i < selectedCards.Count; i++)
		{
			if(selectedCards[i] != null)
			{
				Destroy(selectedCards[i].gameObject);
			}
		}

		for(int i = 0; i < selectedSpells.Count; i++)
		{
			if(selectedSpells[i] != null)
			{
				Destroy(selectedSpells[i].gameObject);
			}
		}

		selectedCards.Clear();
		selectedSpells.Clear();

		cardsLayedout = false;
		spellsLayedout = false;
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");

		if(battleManager.currTurn == BattleStates.PLAYER_TURN)
		{
			opened = true;
			GetComponent<Image>().sprite = cardEye;
			LayOutCards();
			currState = PouchStates.ACTION_CARDS;
			battleManager.currTurn = BattleStates.CHOOSE_CARDS;
			UIBorderBar.Play("BorderSlideIn");
		}
		else if (battleManager.currTurn == BattleStates.CHOOSE_CARDS)
		{

			if(selectedCards.Count > 0 && currState == PouchStates.ACTION_CARDS)
			{
				DisplaySelectedCards();
				battleManager.currTurn = BattleStates.CHOOSE_ENEMIES;

				bool knightless = true;

				for(int i = 0; i < battleManager.enemyList.Count; i++)
				{
					if(battleManager.enemyList[i].myType == EnemyType.KNIGHT)
					{
						knightless = false;
					}
				}

				for(int i = 0; i < battleManager.enemyList.Count; i++)
				{
					if(!knightless)
					{
						if(battleManager.enemyList[i].myType == EnemyType.KING || battleManager.enemyList[i].myType == EnemyType.QUEEN)
						{
							battleManager.enemyList[i].interactable = false;
						}
						else
						{
							battleManager.enemyList[i].interactable = true;
						}
					}
					else
					{
						battleManager.enemyList[i].interactable = true;

					}
				}
			}
		}
		else if (battleManager.currTurn == BattleStates.CHOOSE_ENEMIES)
		{
			for(int i = 0; i < selectedCards.Count; i++)
			{
				if(selectedCards[i].myCard.cardType == CardType.ATTACK)
				{
					if(selectedCards[i].target == null)
					{
						return;
					}
				}
			}

			Attack();
		}

//        if (opened && cardAction)
//        {
//            if (battleManager.target != null && selectedCards.Count > 0)
//            {
//                Attack();
//                opened = false;
//
//            }
//        }
//
//        else if (opened && spellsAction)
//        {
//            if (selectedSpells.Count == 1)
//            {
//                Spells();
//                opened = false;
//
//            }
//        }
//        else if (cardAction)
//        {
//            opened = true;
//            LayOutCards();
//            GetComponent<Image>().sprite = cardEye;
//
//        }
//
//        else if (spellsAction)
//        {
//            opened = true;
//            LayoutSpells();
//            GetComponent<Image>().sprite = spellsEye;
//        }

    }
}
