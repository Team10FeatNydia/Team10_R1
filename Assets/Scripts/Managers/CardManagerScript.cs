using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManagerScript : MonoBehaviour 
{
	#region Singleton
	private static CardManagerScript mInstance;

	public static CardManagerScript Instance
	{
		get
		{
			if(mInstance == null)
			{
				GameObject temp = GameObject.FindGameObjectWithTag("CardManager");

				if(temp == null)
				{
					temp = Instantiate(ManagerControllerScript.Instance.cardManagerPrefab, Vector3.zero, Quaternion.identity);
				}
				mInstance = temp.GetComponent<CardManagerScript>();
			}
			return mInstance;
		}
	}

	public static bool CheckInstanceExist()
	{
		return mInstance;
	}
	#endregion Singleton

	public List<CardDescription> handList = new List<CardDescription>();
	public List<CardDescription> cardList = new List<CardDescription>();
	public GameObject cardPrefab;
	public GameObject cardButton;
	public Transform cardDeckPanel;
	public Transform cardHandPanel;
	public bool spawned = false;
	public GameObject confirmButton;

	void Awake()
	{
		if(CardManagerScript.CheckInstanceExist())
		{
			Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () 
	{
		handList.Clear();

		for(int i = 0; i < PlayerStatSaver.mInstance.cardHandList.Count; i++)
		{
			handList.Add(PlayerStatSaver.mInstance.cardHandList[i]);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(PauseMenuManagerScript.Instance.paused) return;

		if(Input.GetKeyDown(KeyCode.A))
		{
			DisplayDeckPanels();
		}
	}

	public void DisplayDeckPanels()
	{
		if(confirmButton != null)
		{
			if(confirmButton.activeSelf == false)
			{
				confirmButton.SetActive(true);
			}
		}


		//Debug.Log("A");

		for(int i = 0; i < PlayerStatSaver.mInstance.cardHandList.Count; i++)
		{
			PlayerStatSaver.mInstance.cardDeckList.Add(PlayerStatSaver.mInstance.cardHandList[i]);
			PlayerStatSaver.mInstance.cardHandList.Remove(PlayerStatSaver.mInstance.cardHandList[i]);
		}

		if(cardDeckPanel != null)
		{
			//Debug.Log("B");

			if(!cardDeckPanel.gameObject.activeSelf)
			{
				cardDeckPanel.gameObject.SetActive(true);

				for(int i = 0; i < PlayerStatSaver.mInstance.cardDeckList.Count; i++)
				{
					GameObject buttonGO = Instantiate(cardButton, cardDeckPanel.transform);
					CardScript buttonScript = buttonGO.GetComponent<CardScript>();

					buttonScript.myCard = PlayerStatSaver.mInstance.cardDeckList[i];
					buttonGO.GetComponent<Image>().sprite = buttonScript.myCard.cardImage;
				}


			}
			else
			{
				cardDeckPanel.gameObject.SetActive(false);
			}
		}

		if(cardHandPanel != null)
		{
			if(!cardHandPanel.gameObject.activeSelf)
			{
				cardHandPanel.gameObject.SetActive(true);
			}
			else
			{
				cardHandPanel.gameObject.SetActive(false);
			}
		}
	}

	public void ChangeScene()
	{
		if(CardManagerScript.Instance.handList.Count > 4 && CardManagerScript.Instance.handList.Count <= 10) 
		{
			PlayerStatSaver.mInstance.cardHandList.Clear();


			for(int i = 0; i < handList.Count; i++)
			{
				PlayerStatSaver.mInstance.cardHandList.Add(handList[i]);
			}

            GameManagerInstance.instance.ChangeScene (3);
		}
			
	}
}
