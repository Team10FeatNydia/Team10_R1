using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour 
{

    private Image bar;

    // Use this for initialization
    void Start () 
	{
        bar = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update () 
	{

		if (BattleManagerScript.Instance.target != null) 
		{
			bar.fillAmount = (BattleManagerScript.Instance.target.health * 100f / BattleManagerScript.Instance.target.maxHealth) / 100;
		}
    }
}

