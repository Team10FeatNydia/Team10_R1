using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public static bool ChecInstanceExit()
	{
		return mInstance;
	}
	#endregion Singleton

	public List<CardDescription> cardList = new List<CardDescription>();
	public GameObject cardPrefab;

	void Awake()
	{
		if(CardManagerScript.ChecInstanceExit())
		{
			Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(PauseMenuManagerScript.Instance.paused) return;
	}
}
