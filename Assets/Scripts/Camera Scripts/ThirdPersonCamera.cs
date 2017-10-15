using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour 
{
	private Transform lookAt;

	private void Start ()
	{
		lookAt = CameraInstanceManager.instance.target.transform;
	}

	private void LateUpdate ()
	{
		transform.position = Vector3.Lerp (transform.position, lookAt.position, Time.deltaTime);
	}
}