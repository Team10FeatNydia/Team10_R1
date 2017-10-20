using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour 
{
    private Image bar;

	public EnemyStatusScript enemy;

    // Use this for initialization
    void Start () 
	{
        bar = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update () 
	{
		bar.fillAmount = (enemy.health * 100f / enemy.maxHealth) / 100;
    }
}

