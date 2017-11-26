using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FadeManagerScript : MonoBehaviour 
{
	#region Singleton
	private static FadeManagerScript mInstance;

	public static FadeManagerScript Instance
	{
		get
		{
			if(mInstance == null)
			{
				FadeManagerScript temp = ManagerControllerScript.Instance.fadeManager;

				if(temp == null)
				{
					temp = Instantiate(ManagerControllerScript.Instance.fadeManager, Vector3.zero, Quaternion.identity).GetComponent<FadeManagerScript>();
				}
				mInstance = temp;
				ManagerControllerScript.Instance.fadeManager = mInstance;
				//DontDestroyOnLoad(mInstance.gameObject);
			}
			return mInstance;
		}
	}
	public static bool CheckInstanceExist()
	{
		return mInstance;
	}
	#endregion Singleton

	void Awake()
	{
		if(FadeManagerScript.CheckInstanceExist())
		{
			Destroy(this.gameObject);
		}
	}

	public Image fadeImage;
	private bool isInTransition;
	private float transition;
	private bool isShowing;
	private float duration;
	public float fadeInDuration;
	public float fadeOutDuration;
	Canvas thisCanvas;

	public void Fade(bool showing,float duration)
	{
		isShowing = showing;
		isInTransition = true;
		this.duration = duration;
		transition = (isShowing) ? 0 : 1;
	}

	private void Update()
	{
		if (!isInTransition)
			return;
		transition += (isShowing) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
		fadeImage.color = Color.Lerp (new Color (0, 0, 0, 0), Color.black, transition);

		if (transition > 1 || transition < 0) 
			isInTransition = false;
	}

	public void fadeOut()
	{
		fadeImage.enabled = true;
		Fade (true, fadeOutDuration);
	}

	public void fadeIn()
	{
		Fade (false, fadeInDuration);
		Invoke("fadeImageDisable", fadeOutDuration + 0.2f );
		Debug.Log("uidf");
	}

	void fadeImageDisable()
	{
		fadeImage.enabled = false ;
	}
}
