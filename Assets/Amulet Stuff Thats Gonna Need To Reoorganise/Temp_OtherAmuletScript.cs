﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Temp_OtherAmuletScript : MonoBehaviour, IPointerClickHandler
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AmuletStatSaver.mInstance.combatAmuletActive = false;
        Debug.Log("deactive");
    }
}
