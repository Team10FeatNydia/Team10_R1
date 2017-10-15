using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour 
{
	[HideInInspector]
	public PlayerManager self;

	[Header("Movement")]
	private float startYPos;
	private Vector3 endPoint;
	private float duration = 5.0f;

	[Header("BooleanSettings")]
	private bool isTap = false;

	#region Movement
	// Use this for initialization
	void Start () 
	{
		startYPos = self.transform.position.y;
		//startPos = self.transform.position;
		//endPos = self.transform.position + Vector3.forward * distance;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			RaycastHit hit;
			Ray ray;

			ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);

			if(Physics.Raycast(ray, out hit))
			{
				isTap = true;
				endPoint = hit.point;
				endPoint.y = startYPos;
			}
		}

		if(isTap && !Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude))
		{
			gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPoint, 1 / (duration * (Vector3.Distance(gameObject.transform.position, endPoint))));
		}
		else if(isTap && Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude))
		{
			isTap = false;
		}
	}
	#endregion Movement

	#region ChangeScenePlayer
	public void ArenaScene()
	{
		SceneManager.LoadScene(2);
	}
	#endregion ChangeScenePlayer
}

