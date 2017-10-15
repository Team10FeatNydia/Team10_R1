using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveAnalogController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Vector3 initPos;
    Vector3 direction;
    public float moveSpeed;


	// Use this for initialization
	void Start () {
        initPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        direction =this.transform.position - initPos;
        direction.Normalize();
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      this.transform.position = initPos;
        direction = Vector3.zero;
    }
}
