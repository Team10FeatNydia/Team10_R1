using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatusScript : MonoBehaviour 
{
	[HideInInspector]
	public PlayerManager self;

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
    
    [Header("Animation")]
    public PlayerModelAnimationScript playerAnim;

    void Start()
    {
        //health = maxHealth ;
        LoadData();
    }

	void Update()
	{
		if(PauseMenuManagerScript.Instance.paused) return;

		if(isHit)
		{
            playerAnim.Damaged();
		}

        if (isAttacking)
        {
            playerAnim.Attack();
        }

        if (HP <= 0)
        {
            playerAnim.Death();
        }

        SaveData();
	}

	public void Respawn()
	{
		SceneManager.LoadScene(self.respawnScene);
	}

	public void SaveData()
	{
		PlayerStatSaver.mInstance.playerHP = HP;
        //PlayerStatSaver.mInstance.MaxHP = MaxHP;
	}

    public void LoadData()
    {
		HP = PlayerStatSaver.mInstance.playerHP;
        //MaxHP = PlayerStatSaver.mInstance.MaxHP;
    }

	public void Quit()
	{
		SceneManager.LoadScene(self.quitScene);
	}
}
