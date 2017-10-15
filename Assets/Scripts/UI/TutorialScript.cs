using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour 
{
	public enum TutorialType
	{
		Move = 0,
		Jump,
		Interact,
		LMB,
		RMB,
		Roll,
		UseItem
	}

	public TutorialType type;
	public Sprite[] spritePreview;

	void OnEnable()
	{
		if(type == TutorialType.Move)
		{
			GetComponent<Animator>().Play("");
		}
		else if(type == TutorialType.Jump)
		{
			GetComponent<Animator>().Play("");
		}
		else if(type == TutorialType.Interact)
		{
			GetComponent<Animator>().Play("");
		}
		else if(type == TutorialType.LMB)
		{
			GetComponent<Animator>().Play("");
		}
		else if(type == TutorialType.RMB)
		{
			GetComponent<Animator>().Play("");
		}
		else if(type == TutorialType.Roll)
		{
			GetComponent<Animator>().Play("");
		}
		else if(type == TutorialType.UseItem)
		{
			GetComponent<Animator>().Play("");
		}
	}

	void OnValidate()
	{
		GetComponent<SpriteRenderer>().sprite = spritePreview[(int)type];
	}
}
