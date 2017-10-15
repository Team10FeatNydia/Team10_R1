using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManagerScript : MonoBehaviour 
{
	#region Singleton
	private static PauseMenuManagerScript mInstance;

	public static PauseMenuManagerScript Instance
	{
		get
		{
			if(mInstance == null)
			{
				GameObject temp = GameObject.FindGameObjectWithTag("PauseMenuManager");

				if(temp == null)
				{
					temp = Instantiate(ManagerControllerScript.Instance.pauseMenuManagerPrefab, Vector3.zero, Quaternion.identity);
				}
				mInstance = temp.GetComponent<PauseMenuManagerScript>();
			}
			return mInstance;
		}
	}
	public static bool CheckInstanceExit()
	{
		return mInstance;
	}
	#endregion Singleton

	void Awake()
	{
		if(PauseMenuManagerScript.CheckInstanceExit())
		{
			Destroy(this.gameObject);
		}
	}

	public bool paused;
	public GameObject bgmSlider;
	public GameObject sfxSlider;
	public string quitGameScene;

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			paused = !paused;
			if(paused)
			{
				Pause();
			}
			else
			{
				Resume();
			}
		}
	}

	public void Pause()
	{
		paused = true;
		GetComponent<MainMenuManager>().OpenMenu(0);
		GetComponent<MainMenuManager>().SetupBGM(bgmSlider);
		GetComponent<MainMenuManager>().SetupSFX(sfxSlider);
		Time.timeScale = 0f;
	}

	public void Resume()
	{
		paused = false;
		GetComponent<MainMenuManager>().CloseMenu(0);
		Time.timeScale = 1f;
	}

	public void PlayButtonSound()
	{
		SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_UI_BUTTON);
	}
}
