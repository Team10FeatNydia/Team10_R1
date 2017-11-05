using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
	public GameObject player;
	public bool canMoveBack;
	public float movingSpeed;
	public List<PathPoint> points;

	bool isMoving = false;
	int playerPointIndex = 0;
	int targetPointIndex = 0;

	bool isAtPosition = true;
	int currentTargetPointIndex = 0;

	void Start ()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			points.Add (transform.GetChild (i).GetComponent<PathPoint> ());
		}
	}

	void Update ()
	{
		if (isMoving)
		{
			if (isAtPosition)
			{
				ChangePoint ();
			}
			else
			{
				MoveToPoint ();
			}
		}
		else
		{
			if (Input.touchCount == 1)
			{
				StartPath (Input.GetTouch (0).position);
			}
			if (Input.GetMouseButtonDown (0))
			{
				StartPath (Input.mousePosition);
			}
		}
	}

	void StartPath (Vector3 position)
	{
		for (int i = 0; i < points.Count; i++)
		{
			if (points[i].isStoppingPoint)
			{
				if (Vector3.Distance (position, points[i].transform.position) < 50.0f)
				{
					if (canMoveBack || (!canMoveBack && i > playerPointIndex))
					{
						targetPointIndex = i;
						isMoving = true;
						break;
					}
				}
			}
		}
	}

	void MoveToPoint ()
	{
		if (Vector3.Distance (player.transform.position, points[currentTargetPointIndex].transform.position) < 5.0f)
		{
			player.transform.position = points [currentTargetPointIndex].transform.position;
			playerPointIndex = currentTargetPointIndex;
			isAtPosition = true;
		}
		else
		{
			Vector3 direction = points [currentTargetPointIndex].transform.position - player.transform.position;
			direction = Vector3.Normalize (direction);
			// Player translating in x-axis only
			player.transform.Translate (Vector3.right * direction.x * movingSpeed * Time.deltaTime / player.transform.localScale.x);
			// Background translating in y-axis
			transform.parent.Translate (Vector3.down * direction.y * movingSpeed * Time.deltaTime / transform.localScale.y);
		}
	}

	void ChangePoint ()
	{
		if (playerPointIndex == targetPointIndex)
		{
			isMoving = false;
		}
		else
		{
			isAtPosition = false;
			// Move backward
			if (playerPointIndex > targetPointIndex)
			{
				currentTargetPointIndex = playerPointIndex - 1;
			}
			// Move forward
			else if (playerPointIndex < targetPointIndex)
			{
				currentTargetPointIndex = playerPointIndex + 1;
			}
		}
	}
}
