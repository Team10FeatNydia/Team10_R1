using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedOnController : MonoBehaviour 
{
	// MoveTowardsTarget variable 
	public float moveSpeed = 35.0f;
	public GameObject targetObject;
	public bool isFocus = false;
	public bool isMoving = false;

	private float lerpSpeed = 5f;
	private Transform fromRotation;
	private Transform toRotation;
	private Vector3 initialPosition;
	private Quaternion initialRotation;
	private bool inPosition = false;
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			if (!isMoving)
			{
				if (isFocus)
				{
					isFocus = false;
				}
				else
				{
					initialPosition = gameObject.transform.position;
					initialRotation = gameObject.transform.rotation;
					isFocus = true;
					gameObject.GetComponent<ThirdPersonCamera> ().enabled = false;
				}
				fromRotation = gameObject.transform;
				toRotation = targetObject.transform;
				isMoving = true;
			}
		}

		if (isMoving)
		{
			MoveTowardsTarget (targetObject);
		}
		else
		{
			if (!isFocus)
			{
				gameObject.GetComponent<ThirdPersonCamera> ().enabled = true;
			}
		}
	}

	public void MoveTowardsTarget(GameObject target)
	{
		float currentSize = gameObject.GetComponent<Camera> ().orthographicSize;
		if (isFocus)
		{
			if (Mathf.Abs (currentSize - 20.0f) > 1.0f)
			{
				gameObject.GetComponent<Camera> ().orthographicSize = Mathf.Lerp (currentSize, 20.0f, Time.deltaTime * lerpSpeed);
			}
		}
		else
		{
			if (Mathf.Abs (currentSize - 48.5f) > 1.0f)
			{
				gameObject.GetComponent<Camera> ().orthographicSize = Mathf.Lerp (currentSize, 48.5f, Time.deltaTime * lerpSpeed);
			}
		}

		// Move towards focus point
		transform.position = Vector3.MoveTowards (transform.position, target.transform.position, moveSpeed * Time.deltaTime);

		if (!inPosition) 
		{
			if (Vector3.Distance (transform.position, target.transform.position) < 0.1)
			{
				transform.position = target.transform.position;
				inPosition = true;
			}
		}
		else
		{
			if (Mathf.Abs (fromRotation.localEulerAngles.y - toRotation.localEulerAngles.y) < 3) 
			{
				// Reset boolean variables
				transform.rotation = target.transform.rotation;
				inPosition = false;
				isMoving = false;

				if (isFocus)
				{
					targetObject.GetComponent<FocusPoint> ().LockTransform (initialPosition, initialRotation);
				}
				else
				{
					target.GetComponent<FocusPoint> ().UnlockTransform ();
				}
			} 
			else 
			{
				transform.rotation = Quaternion.Lerp (fromRotation.rotation, toRotation.rotation, Time.deltaTime * lerpSpeed);
			}
		}
	}
}
