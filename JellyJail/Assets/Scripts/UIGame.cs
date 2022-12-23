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

    [Header("Inputs")]
    public int currentHealth;
    public int maxHealth;
    
    

    void Start()
    {
        while(ammoCount < maxAmmo)
        {
            ammoArsenal[ammoCount].SetActive(true);
            ammoCount++;
        }

        healthMeter.value = currentHealth;
    }

    public void SetHealth(int recievedHealth)
    {
        healthMeter.value = recievedHealth;
    }

    public void OnClickHealthLoss()
    {
        currentHealth--;
        SetHealth(currentHealth);
    }
    public void OnClickHealthGain()
    {
        currentHealth++;
        SetHealth(currentHealth);
    }
}
