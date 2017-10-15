using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour 
{
	[Header("System")]
    public new MeshCollider collider;
	public new Rigidbody rigidbody;
	public new SpriteRenderer renderer;
	public Animator animator;

	[Header("Developer")]
	public EnemyMovementScript movement;
	public EnemyStatusScript status;

	void Awake()
	{
        collider = GetComponent<MeshCollider>();
		rigidbody = GetComponent<Rigidbody>();
		renderer = GetComponentInChildren<SpriteRenderer>();
		animator = GetComponentInChildren<Animator>();

		movement = GetComponent<EnemyMovementScript>();
		status = GetComponent<EnemyStatusScript>();

		if(movement		!= null) movement.self	= this;
		if(status		!=null)  status.self	= this;
	}
}
