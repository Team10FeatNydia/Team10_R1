using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AmuletScript : MonoBehaviour, IPointerClickHandler
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (AmuletStatSaver.mInstance.combatAmuletActive == false)
        {
            this.gameObject.GetComponent<Image>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<Image>().enabled = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AmuletStatSaver.mInstance.combatAmuletActive = true;
    }
}