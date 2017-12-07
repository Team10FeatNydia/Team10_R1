using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModelAnimationScript : MonoBehaviour {

    Animator animator;

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
    }
}
