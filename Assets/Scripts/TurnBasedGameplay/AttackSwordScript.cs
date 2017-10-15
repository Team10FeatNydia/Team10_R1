using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackSwordScript : MonoBehaviour, IDragHandler, IEndDragHandler
{

	public PlayerStatusScript player;
	public EnemyStatusScript target;

    Vector3 initPos;
    Vector3 direction;
    bool isDragging;

    // Use this for initialization
    void Start()
    {
        initPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            direction = this.transform.position - initPos;
            direction.Normalize();

            Attack();
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        this.transform.position = initPos;
        direction = Vector3.zero;

    }

    void Attack()
    {
        if (target.targeted)
        {
            if (Input.touchCount > 0)
            {

                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    target.health -= player.attack;
                    BattleManagerScript.Instance.currTurn = BattleStates.ENEMY_TURN;
                }               
            }
            
        }
    }
}
