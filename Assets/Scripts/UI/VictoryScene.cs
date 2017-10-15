using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour 
{
	public void Quit()
	{
		Application.Quit();
	}

	public void PlayButtonSound()
	{
		SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_UI_BUTTON);
	}
}
