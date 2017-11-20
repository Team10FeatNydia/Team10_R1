using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
	public bool hide = true;
	public List<GameObject> targets; // list of target points
	public int index = 0;

	Image image;
	int curIndex;
	float timer = 0.0f;

	void Start ()
	{
		image = GetComponent<Image> ();
		curIndex = index;
	}
		
	void Update ()
	{
		UpdateHideStatus (); // should hide or not

		if (!hide) // if not hiding
		{
			UpdateTarget (); // always update to the target point
			Oscillate (); // move up and down
			timer += Time.deltaTime;
		}
	}

	void Oscillate ()
	{
		transform.Translate (Vector3.down * 0.3f * Mathf.Sin (timer)); // move up and down
	}

	public void UpdateTarget ()
	{
		if (index != curIndex) // if not at the correct index
		{
			transform.SetParent (targets [index].transform, false); // change parent to the target point, false means follow the position of target
			transform.SetAsFirstSibling (); // set as first sibling to prevent from blocking the flag
			curIndex = index;
		}
	}

	void UpdateHideStatus ()
	{
		if (hide && image.enabled)
		{
			image.enabled = false;
		}
		else if (!hide && !image.enabled)
		{
			image.enabled = true;
		}
	}
}
