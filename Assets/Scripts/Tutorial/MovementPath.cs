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
			points.Add (transform.GetChild (i).GetComponent<PathPoint> ()); // assign all the children to the list (according to order)
		}
        SetPoint();
	}

	void Update ()
	{
		if (isMoving) // if moving
		{
			if (isAtPosition) // if at the one of the point
			{
				ChangePoint ();
			}
			else // if not at current position, move
			{
				MoveToPoint ();
			}
		}
		else // if currently not moving
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

        PlayerStatSaver.mInstance.playerPoint = playerPointIndex;
	}

	void StartPath (Vector3 position)
	{
		for (int i = 0; i < points.Count; i++) // checking all points
		{
			if (points[i].isStoppingPoint) // if that point is an allowable stopping point
			{
				if (Vector3.Distance (position, points[i].transform.position) < 50.0f) // if the distance of the input and that point is near ( a circle of radius 50units)
				{
					if (canMoveBack || (!canMoveBack && i > playerPointIndex)) // if can move backwards || cannot move backwards but the point direction is forward
					{
						targetPointIndex = i; // set destination to that index
						isMoving = true; // start the moving
						break;
					}
				}
			}
		}
	}

	void MoveToPoint ()
	{
		if (Vector3.Distance (player.transform.position, points[currentTargetPointIndex].transform.position) < 5.0f) // if the player is close enough to that point
		{
			player.transform.position = points [currentTargetPointIndex].transform.position; // teleports player to that point
			playerPointIndex = currentTargetPointIndex; // update player's point index
			isAtPosition = true; // player is now at one of the point
		}
		else // if far from the point
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
		if (playerPointIndex == targetPointIndex) // if player is on the destination point
		{
			isMoving = false; // stop moving
		}
		else // if not, change to the next target point
		{
			isAtPosition = false; // player is not at the position of any point anymore
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

    void SetPoint()
    {
        playerPointIndex = PlayerStatSaver.mInstance.playerPoint;
    }

	public void MoveToAmuletPlace () // Special function for the last event
	{
		targetPointIndex = points.Count - 2;
		isMoving = true;
	}
}
