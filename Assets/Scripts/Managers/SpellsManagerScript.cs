using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsManagerScript : MonoBehaviour
{
    #region Singleton
    private static SpellsManagerScript mInstance;

    public static SpellsManagerScript Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject temp = GameObject.FindGameObjectWithTag("SpellsManager");

                if (temp == null)
                {
                    temp = Instantiate(ManagerControllerScript.Instance.spellsManagerPrefab, Vector3.zero, Quaternion.identity);
                }
                mInstance = temp.GetComponent<SpellsManagerScript>();
            }
            return mInstance;
        }
    }

    public static bool ChecInstanceExit()
    {
        return mInstance;
    }
    #endregion Singleton

    public List<SpellsDescription> spellsList = new List<SpellsDescription>();
    public GameObject spellsPrefab;

    void Awake()
    {
        if (SpellsManagerScript.ChecInstanceExit())
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenuManagerScript.Instance.paused) return;
    }
}
