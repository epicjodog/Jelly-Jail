using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    public static GlobalController Instance { get; private set; }

    public int currentHealth = 4;
    public int maxBullets = 3;
    public int tokens = 0;
    public int currentLevel = 1;

    public bool jellyMagnet; //Increased pickup range of Gel --done
    public bool TreasureMagnet; //Increased pickup range of Tokens
    public bool panicOrb; //Longer invincibility after taking damage
    public bool slimenip; //Guaranteed shop + 5 Token discount on purchases
    public bool tinyAnvil; //Increases dropkick damage
    //public bool lilBuddy; 
    public int lilBuddy = 0; //Shoot two Gel at once
    public bool obviousBomb; //Gel shots explode(instead of 1 enemy, hits all in range) (increase hit box)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void ResetUpgrades()
    {
        currentHealth = 4;
        maxBullets = 3;
        tokens = 0;

        jellyMagnet = false;
        TreasureMagnet = false;
        panicOrb = false;
        slimenip = false;
        tinyAnvil = false;
        lilBuddy = 0;
        obviousBomb = false;
    }
}
