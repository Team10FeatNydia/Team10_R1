using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FadeManagerScript : MonoBehaviour {

	public static FadeManagerScript Instance{ set; get;}

	public Image fadeImage;
	public bool isInTransition;
	public float transition;
	public bool isShowing;
	public float duration;

	public void Awake()

	{
		Instance = this;
	}

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
		Fade (true, 1.5f);
	}

	public void fadeIn()
	{
		Fade (false, 1.5f);
	}

}
