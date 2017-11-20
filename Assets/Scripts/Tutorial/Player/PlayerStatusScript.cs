using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatusScript : MonoBehaviour 
{
	[HideInInspector]
	public PlayerManager self;
    Animator animator;

	[Header("Stats")]
	public PlayerStatistics localPlayerData = new PlayerStatistics();
    // public int MaxHP = 100;
    public float HP;
//	public int manaPoints;


//	[Header("Movement")]
//	public float movementSpeed;
//	public float jumpHeight;

	[Header("Combat")]
	public bool isHit;
    public bool isAttacking;
	public int attack;

	[Header("Particle System Effect")]
	public ParticleSystem enemyAttack;
	public ParticleSystem enemyHeavyAttack;
	public ParticleSystem healingPlayer;


    void Start()
    {
        //health = maxHealth ;
        LoadData();
        animator = GetComponent<Animator>();
    }

	void Update()
	{
		if(PauseMenuManagerScript.Instance.paused) return;

		if(isHit)
		{
            animator.SetTrigger("damaged");
		}

        if (isAttacking)
        {
            animator.SetTrigger("attack");
        }

        if (HP <= 0)
        {
            animator.SetTrigger("death");
        }

        SaveData();
	}

	public void Respawn()
	{
		SceneManager.LoadScene(self.respawnScene);
	}

	public void SaveData()
	{
        PlayerStatSaver.mInstance.HP = HP;
        //PlayerStatSaver.mInstance.MaxHP = MaxHP;
	}

    public void LoadData()
    {
        HP = PlayerStatSaver.mInstance.HP;
        //MaxHP = PlayerStatSaver.mInstance.MaxHP;
    }

	public void Quit()
	{
		SceneManager.LoadScene(self.quitScene);
	}
}
