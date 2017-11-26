using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerInstance : MonoBehaviour
{
	[HideInInspector]
	public static GameManagerInstance instance;

	public PlayerStatSaver playerStatSaver;
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

		playerStatSaver = PlayerStatSaver.mInstance;
		DontDestroyOnLoad(this);
	}

	void Start ()
	{
		FadeManagerScript.Instance.fadeIn();
		
		for (int i = 0; i < levelOne.Count; i++)
		{
			DontDestroyOnLoad (levelOne [i].gameObject);
		}
	}

	public void ChangeScene (int index)
    {
        if (index == 0)
        {
            for (int i = 0; i < levelOne.Count; i++)
            {
                levelOne[i].gameObject.SetActive(false);
            }
            SceneManager.LoadScene(0);
        }

        else if (index == 1)
        {
            for (int i = 0; i < levelOne.Count; i++)
            {
                levelOne[i].gameObject.SetActive(false);
            }
            SceneManager.LoadScene(1);
        }

        else if (index == 2)
		{
			for (int i = 0; i < levelOne.Count; i++)
			{
				levelOne [i].gameObject.SetActive (false);
			}
			SceneManager.LoadScene (2);
		}

        else if (index == 3)
        {
            for (int i = 0; i < levelOne.Count; i++)
            {
                levelOne[i].gameObject.SetActive(false);
            }
            SceneManager.LoadScene(3);
        }

        else if (index == 4)
        {
            for (int i = 0; i < levelOne.Count; i++)
            {
                levelOne[i].gameObject.SetActive(false);
            }
            SceneManager.LoadScene(4);
        }

        else if (index == 5)
        {
            for (int i = 0; i < levelOne.Count; i++)
            {
                levelOne[i].gameObject.SetActive(false);
            }
            SceneManager.LoadScene(5);
        }
    }
}
