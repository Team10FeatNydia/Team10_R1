using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelAnimationScript : MonoBehaviour {

    Animator animator;
    public PlayerStatusScript player;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Attack()
    {
        animator.SetTrigger("attack");
    }

    public void Damaged()
    {
        animator.SetTrigger("damaged");
        player.isHit = false;
    }

    public void Death()
    {
        animator.SetTrigger("death");
    }
}
