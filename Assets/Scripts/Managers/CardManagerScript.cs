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

		if(cardDeckPanel != null)
		{
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
}
