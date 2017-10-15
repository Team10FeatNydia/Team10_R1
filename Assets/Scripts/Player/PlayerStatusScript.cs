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
//    public int maxHealth = 100;
//	public int health;
//	public int manaPoints;


//	[Header("Movement")]
//	public float movementSpeed;
//	public float jumpHeight;

	[Header("Combat")]
	public bool isHit;
	public int attack;
	public float invincibleTimer;
	public float invincibleDuration;


    void Start()
    {
        //health = maxHealth ;
    }

	void Update()
	{
		if(PauseMenuManagerScript.Instance.paused) return;

		if(isHit)
		{
			if(invincibleTimer < invincibleDuration)
			{
				invincibleTimer += Time.deltaTime;
			}
			else
			{
				invincibleTimer = 0;
				isHit = false;
			}
		}
	}

	public void Respawn()
	{
		SceneManager.LoadScene(self.respawnScene);
	}

	public void SaveData()
	{
		GameManagerScript.Instance.savedPlayerData = localPlayerData;
	}

	public void Quit()
	{
		SceneManager.LoadScene(self.quitScene);
	}

	public void ApplyInvicibility()
	{
		if(!isHit)
		{
			isHit = true;
			invincibleTimer = 0.0f;
			//Player receive damage sound script
			//SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_PL_RECEIVEDMG);
		}
	}
}
