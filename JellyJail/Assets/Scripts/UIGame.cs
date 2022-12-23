using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [Header("Ammo")]
    public GameObject[] ammoArsenal;
    private int ammoCount = 0;

    public int currentAmmo;
    public int maxAmmo;

    [Header("Health")]
    public Slider healthMeter;
    public GameObject heartDead;
    public GameObject heartAlive;

    [Header("Inputs")]
    private int currentHealth;
    public int maxHealth;


    void Start()
    {
        maxAmmo = GlobalController.Instance.maxBullets;

        while(ammoCount < maxAmmo)
        {
            ammoArsenal[ammoCount].SetActive(true);
            ammoCount++;
        }
        currentAmmo = ammoCount;
        print(currentAmmo);

        currentHealth = GlobalController.Instance.currentHealth;

        healthMeter.value = currentHealth;
        healthMeter.maxValue = maxHealth;
    }

    /////////////////////////////////////////////////////////////////////////////////
    // Health Functions
    public void HealthSetCurrent(int recievedHealth)
    {
        if(currentHealth == 0)
        {
            heartDead.SetActive(true);
            heartAlive.SetActive(false);
        }

        healthMeter.value = recievedHealth;
    }

    public void HealthSetMax(int recievedMaxHP)
    {
        healthMeter.maxValue = recievedMaxHP;
    }

    public void HealthRevive()
    {
        // Resets back to full HP and moves out of death icon

        heartDead.SetActive(false);
        heartAlive.SetActive(true);
        currentHealth = maxHealth;
        healthMeter.value = maxHealth;
    }

    /////////////////////////////////////////////////////////////////////////////////
    // Ammo Stuff
    public void AmmoLoss()
    {
        if (currentAmmo <= 0) return;
        ammoArsenal[currentAmmo - 1].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        currentAmmo--;
    }
    public void AmmoGain()
    {
        if (currentAmmo >= maxAmmo) return;
        ammoArsenal[currentAmmo].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        currentAmmo++;
    }
    
    public void AmmoInc()
    {
        while(ammoCount < maxAmmo)
        {
            ammoArsenal[ammoCount].SetActive(true);
            ammoCount++;
        }
        currentAmmo = ammoCount;
    }

    /////////////////////////////////////////////////////////////////////////////////
    // Testing Purposes
    public void OnClickHealthLoss()
    {
        currentHealth--;
        HealthSetCurrent(currentHealth);
    }
    public void OnClickHealthGain()
    {
        currentHealth++;
        HealthSetCurrent(currentHealth);
    }
    public void OnClickAmmoGain()
    {
        AmmoGain();
    }
    public void OnClickAmmoLoss()
    {
        AmmoLoss();
    }
}
