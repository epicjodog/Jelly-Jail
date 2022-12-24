using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIShop : MonoBehaviour
{
    /*
     * Bandage - 0
     * Royal Fondue - 1
     * Gel-In-A-Can - 2
     * Medicine - 3
     * Power Potion - 4
     * Slicecream - 5
    */
    [Header("List of Items (keep in the order of the item displays)")]
    [SerializeField] string[] itemTitle; //title of each item
    [SerializeField] int[] itemPrices; //price of each item
    [SerializeField] string[] itemDescriptions; //description of each item

    [Header("Item Cards")]
    [SerializeField] TextMeshProUGUI[] itemTitleText; //0: card 1, 1: card 2, 2: card 3
    [SerializeField] TextMeshProUGUI[] itemCostText; //price text on each card
    [SerializeField] TextMeshProUGUI[] itemDescriptionText; //description text on each card

    [Header("Item Displays")]
    [SerializeField] GameObject[] itemDisplayOne; //GOs of images of each item for card 1
    [SerializeField] GameObject[] itemDisplayTwo; //card 2
    [SerializeField] GameObject[] itemDisplayThree; //card 3

    [Header("Sir Catto")]
    [SerializeField] Animator shopkeeperAnim;
    [Header("Tokens")]
    [SerializeField] TextMeshProUGUI tokenText;

    int[] chosenItem = new int[3]; //the chosen item for each card, refer to lines 9-16

    PlayerMovement player;
    AudioManager audioMan;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerController").GetComponent<PlayerMovement>();
        tokenText.text = GlobalController.Instance.tokens.ToString();
        audioMan = GetComponent<AudioManager>();
        gameObject.SetActive(false);

        if(GlobalController.Instance.slimenip)
        {
            for (int i = 0; i < itemPrices.Length - 1; i++)
            {
                itemPrices[i] -= 5;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            CreateItem(i);
        }
    }

    void CreateItem(int cardNumber) //0, 1, 2
    {
        chosenItem[cardNumber] = Random.Range(0, 5);
        for (int i = 0; i < 3; i++)
        {
            while (chosenItem[i] == chosenItem[cardNumber] && i != cardNumber)
            {
                chosenItem[cardNumber] = Random.Range(0, 5);
            }
        }

        itemTitleText[cardNumber].text = itemTitle[chosenItem[cardNumber]];
        itemCostText[cardNumber].text = itemPrices[chosenItem[cardNumber]].ToString();
        itemDescriptionText[cardNumber].text = itemDescriptions[chosenItem[cardNumber]];
        switch (cardNumber)
        {
            case 0:
                itemDisplayOne[chosenItem[cardNumber]].SetActive(true);
                break;
            case 1:
                itemDisplayTwo[chosenItem[cardNumber]].SetActive(true);
                break;
            case 2:
                itemDisplayThree[chosenItem[cardNumber]].SetActive(true);
                break;
            default:
                break;
        }

    }

    public void BuyItem(int cardNumber) //0, 1, 2
    {
        if(GlobalController.Instance.tokens >= itemPrices[chosenItem[cardNumber]])
        {
            GlobalController.Instance.tokens -= itemPrices[chosenItem[cardNumber]];
            audioMan.Play("Buy");
            shopkeeperAnim.SetTrigger("CattoBuy");
            GiveBuff(chosenItem[cardNumber]);
            tokenText.text = GlobalController.Instance.tokens.ToString();
        }
        else
        {
            //can't buy
        }
    }

    void GiveBuff(int item) //chosenItem
    {
        switch (item)
        {
            case 0: //Bandage
                GlobalController.Instance.currentHealth += 1;     
                break;
            case 1: //Royal Fondue
                GlobalController.Instance.maxBullets += 2;
                GlobalController.Instance.currentHealth += 2;
                break;
            case 2: //GelCan
                GlobalController.Instance.maxBullets += 1;
                break;
            case 3: //Medicine
                GlobalController.Instance.currentHealth += 2;
                break;
            case 4: //Power Potion
                GlobalController.Instance.maxBullets += 1;
                GlobalController.Instance.currentHealth += 1;
                break;
            case 5: //Slicecream
                GlobalController.Instance.currentHealth += 3;
                break;
        }
        player.UpdateTextShop();
    }
}
