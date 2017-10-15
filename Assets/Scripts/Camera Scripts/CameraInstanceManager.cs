using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInstanceManager : MonoBehaviour 
{
	public static CameraInstanceManager instance;
	public GameObject target;
	public bool isTargetMoving;

	private ThirdPersonCamera tpc;
//	private DragMoveController dmc;
//	private LockedOnController loc;

	private void Awake () 
	{
		instance = this;
		isTargetMoving = false;
	}

	private void Start ()
	{
		tpc = GetComponent<ThirdPersonCamera> ();
//		dmc = GetComponent<DragMoveController> ();
//		loc = GetComponent<LockedOnController> ();
	}

	private void Update ()
	{
		if (isTargetMoving)
		{
			if (!tpc.enabled)
			{
				tpc.enabled = true;
			}
//
//			if (dmc.enabled)
//			{
//				dmc.enabled = false;
//			}
//
//			if (loc.enabled)
//			{
//				loc.enabled = false;
//			}
		}
//		else 
//		{
//			if (tpc.enabled)
//			{
//				tpc.enabled = false;
//			}
//
//			if (Input.touchCount == 2)
//			{
//				if (!loc.isFocus && !loc.isAdjusting) // If focus cannot drag
//				{
//					if (!dmc.enabled)
//					{
//						dmc.enabled = true;
//					}
//
//					if (loc.enabled)
//					{
//						loc.enabled = false;
//					}
//				}
//			}
//			else if (Input.touchCount == 3)
//			{
//				if (dmc.enabled)
//				{
//					dmc.enabled = false;
//				}
//
//				if (!loc.enabled)
//				{
//					loc.enabled = true;
//				}
//			}
//		}
	}
}
