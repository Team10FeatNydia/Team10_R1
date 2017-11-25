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
    public float HP;

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

		LoadData();
        //SaveData();
	}

	public void Respawn()
	{
		SceneManager.LoadScene(self.respawnScene);
	}

	public void SaveData()
	{
		PlayerStatSaver.mInstance.playerHP = HP;
	}

    public void LoadData()
    {
		HP = PlayerStatSaver.mInstance.playerHP;
    }

	public void Quit()
	{
		SceneManager.LoadScene(self.quitScene);
	}
}
