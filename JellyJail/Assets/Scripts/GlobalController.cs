using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    public static GlobalController Instance { get; private set; }

    public int currentHealth = 4;
    public int maxBullets = 3;
    public int tokens;

    public bool jellyMagnet; //Increased pickup range of Gel
    public bool TreasureMagnet; //Increased pickup range of Tokens
    public bool panicOrb; //Longer invincibility after taking damage
    public bool slimenip; //Guaranteed shop + 5 Token discount on purchases
    public bool tinyAnvil; //Increases dropkick damage
    public bool lilBuddy; //Shoot two Gel at once
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
}
