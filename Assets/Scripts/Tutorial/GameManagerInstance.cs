using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerInstance : MonoBehaviour
{
	[HideInInspector]
	public static GameManagerInstance instance;

	public GameData gameData;
	public List<GameObject> levelOne;
	public List<GameObject> arenaScene;

	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		gameData = GetComponent<GameData> ();
		DontDestroyOnLoad (this);
	}

	void Start ()
	{
		for (int i = 0; i < levelOne.Count; i++)
		{
			DontDestroyOnLoad (levelOne [i].gameObject);
		}
	}

	void Update ()
	{

	}

	public void ChangeScene (int index)
	{
		if (index == 2)
		{
			for (int i = 0; i < levelOne.Count; i++)
			{
				levelOne [i].gameObject.SetActive (false);
			}
			SceneManager.LoadScene (2);
			Debug.Log (SceneManager.GetActiveScene ().buildIndex);
		}
	}
}
