using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    [SerializeField] int hamShankPrice;
    [SerializeField] int jellyMagnetPrice;
    [SerializeField] int lilBuddyPrice;
    [SerializeField] int obviousBombPrice;
    [SerializeField] int panicOrbPrice;
    [SerializeField] int slimenipPrice;
    [SerializeField] int tinyAnvilPrice;
    [SerializeField] int treasureMagnetPrice;
    [SerializeField] int bandagePrice;
    [SerializeField] int gelInACanPrice;
    [SerializeField] int medicinePrice;
    [SerializeField] int powerPotionPrice;
    [SerializeField] int royalFonduePrice;
    [SerializeField] int slicecreamPrice;

    PlayerMovement player;
    AudioManager audioMan;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerController").GetComponent<PlayerMovement>();
    }

    public void BuyHamShank()
    {
        if(GlobalController.Instance.tokens >= hamShankPrice)
        {
            if (player.health <= 16) GlobalController.Instance.currentHealth += 4;
            else GlobalController.Instance.currentHealth = 20;
            GlobalController.Instance.tokens -= hamShankPrice;
            audioMan.Play("Buy");
            //disable button
        }
        else
        {
            //can't afford
        }
    }
    public void BuyJellyMagnet()
    {
        if (GlobalController.Instance.tokens >= jellyMagnetPrice)
        {
            GlobalController.Instance.jellyMagnet = true;
            audioMan.Play("Buy");
            GlobalController.Instance.tokens -= jellyMagnetPrice;
            //disable button
        }
        else
        {
            //can't afford
        }
    }
    public void BuyLilBuddy()
    {
        if (GlobalController.Instance.tokens >= lilBuddyPrice)
        {
            GlobalController.Instance.lilBuddy = true;
            audioMan.Play("Buy");
            GlobalController.Instance.tokens -= lilBuddyPrice;
            //disable button
        }
        else
        {
            //can't afford
        }
    }
    public void BuyObviousBomb()
    {
        if (GlobalController.Instance.tokens >= obviousBombPrice)
        {
            GlobalController.Instance.obviousBomb = true;
            audioMan.Play("Buy");
            GlobalController.Instance.tokens -= obviousBombPrice;
            //disable button
        }
        else
        {
            //can't afford
        }
    }
    public void BuyPanicOrb()
    {
        if (GlobalController.Instance.tokens >= panicOrbPrice)
        {
            GlobalController.Instance.panicOrb = true;
            audioMan.Play("Buy");
            GlobalController.Instance.tokens -= panicOrbPrice;
            //disable button
        }
        else
        {
            //can't afford
        }
    }
    public void BuySlimenip()
    {
        if (GlobalController.Instance.tokens >= slimenipPrice)
        {
            GlobalController.Instance.slimenip = true;
            audioMan.Play("Buy");
            GlobalController.Instance.tokens -= slimenipPrice;
            //disable button
        }
        else
        {
            //can't afford
        }
    }
    public void BuyTinyAnvil()
    {
        if (GlobalController.Instance.tokens >= tinyAnvilPrice)
        {
            GlobalController.Instance.tinyAnvil = true;
            audioMan.Play("Buy");
            GlobalController.Instance.tokens -= tinyAnvilPrice;
            //disable button
        }
        else
        {
            //can't afford
        }
    }
    public void BuyTreasureMagnet()
    {
        if (GlobalController.Instance.tokens >= treasureMagnetPrice)
        {
            GlobalController.Instance.TreasureMagnet = true;
            audioMan.Play("Buy");
            GlobalController.Instance.tokens -= treasureMagnetPrice;
            //disable button
        }
        else
        {
            //can't afford
        }
    }
    public void BuyBandage()
    {
        if (GlobalController.Instance.tokens >= bandagePrice && player.health > 20)
        {
            GlobalController.Instance.currentHealth += 1;
            audioMan.Play("Buy");
            GlobalController.Instance.tokens -= bandagePrice;
            //disable button
        }
        else
        {
            //can't afford
        }
    }
    public void BuyGelInACan()
    {
        if (GlobalController.Instance.tokens >= gelInACanPrice)
        {
            GlobalController.Instance.maxBullets += 1;
            audioMan.Play("Buy");
            GlobalController.Instance.tokens -= gelInACanPrice;
            //disable button
        }
        else
        {
            //can't afford
        }
    }
    public void BuyMedicine()
    {
        if (GlobalController.Instance.tokens >= medicinePrice)
        {
            GlobalController.Instance.currentHealth += 2;
            audioMan.Play("Buy");
            GlobalController.Instance.tokens -= medicinePrice;
            //disable button
        }
        else
        {
            //can't afford
        }
    }
    public void BuyPowerPotion()
    {
        if (GlobalController.Instance.tokens >= powerPotionPrice)
        {
            GlobalController.Instance.maxBullets += 1;
            GlobalController.Instance.currentHealth += 1;
            audioMan.Play("Buy");
            GlobalController.Instance.tokens -= powerPotionPrice;
            //disable button
        }
        else
        {
            //can't afford
        }
    }
    public void BuyRoyalFondue()
    {
        if (GlobalController.Instance.tokens >= royalFonduePrice)
        {
            GlobalController.Instance.maxBullets += 2;
            GlobalController.Instance.currentHealth += 2;
            audioMan.Play("Buy");
            GlobalController.Instance.tokens -= royalFonduePrice;
            //disable button
        }
        else
        {
            //can't afford
        }
    }
    public void BuySlicecream()
    {
        if (GlobalController.Instance.tokens >= slicecreamPrice)
        {
            GlobalController.Instance.currentHealth += 3;
            audioMan.Play("Buy");
            GlobalController.Instance.tokens -= slicecreamPrice;
            //disable button
        }
        else
        {
            //can't afford
        }
    }
    public void Exit()
    {

    }


}
