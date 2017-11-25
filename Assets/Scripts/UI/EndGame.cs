using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour 
{

	public void MainMenu()
	{
		SceneManager.LoadScene(0);
    }

	public void PlayButtonSound()
	{	
		SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_UI_BUTTON);
	}


}
