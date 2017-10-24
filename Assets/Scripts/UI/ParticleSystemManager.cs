using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour 
{
	public static ParticleSystemManager instance;

	public GameObject target;
	private ParticleSystem ps;

	void Awake()
	{
		instance = this;
		ps = gameObject.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!target)
		{
			if(ps.isPlaying)
			{
				ps.Stop();
			}
		}
		else if(target.CompareTag("Enemy"))
		{
			if(ps.isStopped)
			{
				ps.Play();
			}
			gameObject.transform.position = target.transform.position;
		}
	}
}
