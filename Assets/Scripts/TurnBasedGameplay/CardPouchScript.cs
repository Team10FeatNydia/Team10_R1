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
	Image myEye;
    public Sprite cardEye;
    public Sprite spellsEye;
    public Sprite defaultEye;
	public GameObject arrowButton;
    public GameObject manaIcon;
    public Vector2 fingerStartPos = Vector2.zero;
    public float minSwipeDist = 10.0f;
	public float delay = 0.0f;
    public bool isSwipe = false;
    public bool opened = false;
    public bool cardAction;
    public bool spellsAction;
    public int manaCheck;
	public Animator UIBorderBar;

	bool cardsLayedout = false;
//	bool spellsLayedout = false;
	PouchStates currState;
//    private int spellsAttack = 0;
//    private bool spellsAttackbool = false;
//    private bool spellsHealbool = false;
    private float touch1;
    private Sprite currentEye;

    // Use this for initialization
    void Start()
    {
		myEye = GetComponent<Image>();
        battleManager = BattleManagerScript.Instance;
        GetComponent<Image>().sprite = defaultEye;
    }

    void Attack()
    {
        for (int i = 0; i < selectedCards.Count; i++)
        {
			selectedCards[i].StartHop(transform.parent.position - new Vector3(-200f, -100f, 0f), 0.8f, 100f, 1.0f * i);

			//selectedCards[i].Invoke("Attack", 0.8f * i);

			battleManager.player.localPlayerData.manaPoints -= selectedCards[i].myCard.manaCost;

        }

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

        battleManager.currTurn = BattleStates.ENEMY_TURN;
		battleManager.enemyAction = true;
		myEye.sprite = defaultEye;

		UIBorderBar.Play("BorderSlideOut");
		arrowButton.SetActive(false);
    }

	void LayOutCards()
    {
		manaCheck = battleManager.player.localPlayerData.manaPoints;
        selectedCards.Clear();

		for(int i = 0; i < displayedSpells.Length; i++)
		{
			if(displayedSpells[i] != null) displayedSpells[i].GetComponent<SpellsScript>().HideSelf();
		}

		for (int i = 0; i < PlayerStatSaver.mInstance.cardDeckList.Count; i++)
        {
            CardDescription tempCard;
			tempCard = PlayerStatSaver.mInstance.cardDeckList[i];
            tempCard.isSpawned = false;
			PlayerStatSaver.mInstance.cardDeckList[i] = tempCard;
        }

		if(PlayerStatSaver.mInstance.cardDeckList.Count < 5)
		{
			for (int i = 0; i < PlayerStatSaver.mInstance.cardDeckList.Count; i++)
			{
				bool exitLoop = false;
				CardDescription tempCard;

				int randNum;

				do
				{
					randNum = Random.Range(0, PlayerStatSaver.mInstance.cardDeckList.Count);

					if (!PlayerStatSaver.mInstance.cardDeckList[randNum].isSpawned)
					{
						tempCard = PlayerStatSaver.mInstance.cardDeckList[randNum];
						tempCard.isSpawned = true;
						PlayerStatSaver.mInstance.cardDeckList[randNum] = tempCard;
						exitLoop = true;
					}
				} while (!exitLoop);

				GameObject newCard = Instantiate(CardManagerScript.Instance.cardPrefab, this.transform) as GameObject;

				CardScript cardScript = newCard.GetComponent<CardScript>();
				cardScript.myCard = PlayerStatSaver.mInstance.cardDeckList[randNum];
				cardScript.cardPouch = this;

				Transform myTransform = newCard.GetComponent<RectTransform>();
				Vector3 originPos = newCard.GetComponent<RectTransform>().localPosition;
				Vector3 endPos;

				myTransform.localPosition = new Vector3(-90f * i - 90f, 0f, 0f);

				endPos = newCard.GetComponent<RectTransform>().localPosition;

				cardScript.SetLerpPos(myTransform.TransformPoint(endPos), true, false);


				cardScript.origin = newCard.GetComponent<RectTransform>().localPosition;
				newCard.transform.SetParent(this.transform.parent, true);

				displayedCards[i] = newCard;
				displayedCards[i].GetComponent<CardScript>().UpdateStats();

				cardsLayedout = true;
			}
		}
		else
		{
			for (int i = 0; i < 5; i++)
			{
				bool exitLoop = false;
				CardDescription tempCard;

				int randNum;

				do
				{
					randNum = Random.Range(0, PlayerStatSaver.mInstance.cardDeckList.Count);

					if (!PlayerStatSaver.mInstance.cardDeckList[randNum].isSpawned)
					{
						tempCard = PlayerStatSaver.mInstance.cardDeckList[randNum];
						tempCard.isSpawned = true;
						PlayerStatSaver.mInstance.cardDeckList[randNum] = tempCard;
						exitLoop = true;
					}
				} while (!exitLoop);

				GameObject newCard = Instantiate(CardManagerScript.Instance.cardPrefab, this.transform) as GameObject;

				CardScript cardScript = newCard.GetComponent<CardScript>();
				cardScript.myCard = PlayerStatSaver.mInstance.cardDeckList[randNum];
				cardScript.cardPouch = this;

				Transform myTransform = newCard.GetComponent<RectTransform>();
				Vector3 originPos = newCard.GetComponent<RectTransform>().localPosition;
				Vector3 endPos;

				myTransform.localPosition = new Vector3(-90f * i - 90f, 0f, 0f);

				endPos = newCard.GetComponent<RectTransform>().localPosition;

				cardScript.SetLerpPos(myTransform.TransformPoint(endPos), true, false);

				cardScript.origin = newCard.GetComponent<RectTransform>().localPosition;
				newCard.transform.SetParent(this.transform.parent, true);

				displayedCards[i] = newCard;
				displayedCards[i].GetComponent<CardScript>().UpdateStats();

				cardsLayedout = true;
			}
		}
    }

	public void DisplaySelectedCards()
	{
		for(int i = 0; i < displayedCards.Length; i++)
		{
			if(displayedCards[i] != null)
			{
				if(!displayedCards[i].GetComponent<CardScript>().selected)
				{
					Destroy(displayedCards[i]);
				}
			}
		}

		List<CardScript> targetCards = new List<CardScript>();
		List<CardScript> nontargetCards = new List<CardScript>();

		for(int i = 0; i < selectedCards.Count; i++)
		{
			if(selectedCards[i].myCard.cardType != CardType.ATTACK && selectedCards[i].myCard.cardType != CardType.STUN)
			{
				nontargetCards.Add(selectedCards[i]);
			}
			else
			{
				targetCards.Add(selectedCards[i]);
			}

			//Needs to be checked
			selectedCards[i].rectTransform.rotation = Quaternion.identity;

			selectedCards[i].selected = false;
		}

		for(int i = 0; i < targetCards.Count; i++)
		{
			targetCards[i].GetComponent<RectTransform>().localPosition = new Vector3(-180f * i - 220f - 104f, 113.75f, 0f);

		}

		for(int i = 0; i < nontargetCards.Count; i++)
		{
			nontargetCards[i].interactable = false;
			nontargetCards[i].GetComponent<Image>().color = Color.white;
			nontargetCards[i].rectTransform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
			nontargetCards[i].GetComponent<RectTransform>().localPosition = new Vector3(-180f * (i+targetCards.Count) - 220f - 104f, 113.75f, 0f);
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
	}

	void manaActive()
	{
        manaIcon.SetActive(true);
	}

	public void SlideInCards()
	{
		for(int i = 0; i < battleManager.enemyList.Count; i++)
		{
			battleManager.enemyList[i].blueTarget.Stop();
			battleManager.enemyList[i].redTarget.Stop();
		}

		battleManager.manaText.text = battleManager.player.localPlayerData.manaPoints.ToString();
		DestroyCards();
		battleManager.currTurn = BattleStates.PLAYER_TURN;
		myEye.sprite = defaultEye;

		UIBorderBar.Play("BorderSlideOut");
		arrowButton.SetActive(false);
	}

    public void OnPointerClick(PointerEventData eventData)
    {
		if(TutorialManagerScript.mInstance.tutorialOpened)
		{
			return;
		}

		if(battleManager.currTurn == BattleStates.PLAYER_TURN)
		{
			DestroyCards();

			opened = true;
			currentEye = cardEye;
			Invoke("LayOutCards" , delay);
			Invoke("manaActive" , delay);
			currState = PouchStates.ACTION_CARDS;
			battleManager.currTurn = BattleStates.CHOOSE_CARDS;
			UIBorderBar.Play("BorderSlideIn");
			arrowButton.SetActive(true);
			GetComponent<Animator>().Play("StartEyeAnimation");

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
							battleManager.enemyList[i].blueTarget.Stop();
							battleManager.enemyList[i].redTarget.Stop();
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
				if(selectedCards[i].myCard.cardType == CardType.ATTACK || selectedCards[i].myCard.cardType == CardType.STUN)
				{
					if(selectedCards[i].target == null)
					{
						return;
					}
				}
			}

			Attack();
		}
    }
}