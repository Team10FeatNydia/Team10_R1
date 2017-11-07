using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatUI : MonoBehaviour 
{

    public PlayerStatusScript player;
    public Text playerManaCount;
    public Text lockedEnemyState;
    public Text lockedEnemyHealth;
    public Text playerHealthText;
	public string sceneName;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
        UpdateBattleUI();
	}

   

    void UpdateBattleUI()
    {
        playerManaCount.text = player.localPlayerData.manaPoints.ToString();

		playerHealthText.text = player.localPlayerData.health + "/" + player.localPlayerData.maxHealth.ToString();



        if (BattleManagerScript.Instance.target != null) {

            lockedEnemyHealth.enabled = true;
            lockedEnemyState.text = "Enemy Locked On";
            lockedEnemyHealth.text = BattleManagerScript.Instance.target.health + "/" + BattleManagerScript.Instance.target.maxHealth.ToString ();
      
        } 
        else 
        {
            lockedEnemyState.text = "Please select an enemy";

            lockedEnemyHealth.enabled = false;
        }
    }
}
