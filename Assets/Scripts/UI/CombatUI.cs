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
   // public Image bar;



	// Use this for initialization
	void Start () {
        
     
        //bar = GetComponent<Image>();

	}
	
	// Update is called once per frame
	void Update () {
        
        UpdateBattleUI();

       // bar.fillAmount = ( player.health * 100f / player.maxHealth ) / 100;

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
           // lockedEnemyHealth.color = Color.Lerp(lockedEnemyHealth.color, Color.red, Time.deltaTime);

            if(BattleManagerScript.Instance.enemyList.Count <= 0)
            {
				SceneManager.LoadScene(sceneName);
            }
        }
    }

}
