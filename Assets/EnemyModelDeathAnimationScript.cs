using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModelDeathAnimationScript : MonoBehaviour {

    Animator animator;
    bool isDead;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Death()
    {
        animator.SetTrigger("death");
    }
}
