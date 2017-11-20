using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour 
{
	public void MainMenu()
	{
        GameManagerInstance.instance.ChangeScene(0);
    }

	public void PlayButtonSound()
	{
		SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_UI_BUTTON);
	}
}
