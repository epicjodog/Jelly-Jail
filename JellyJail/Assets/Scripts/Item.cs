using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    enum WhichItem
    {
        HamShank,
        JellyMagnet,
        LilBuddy,
        ObviousBomb,
        PanicOrb,
        Slimenip,
        TinyAnvil,
        TreasureMagnet
    }
    [SerializeField] WhichItem whichItem;
    [SerializeField] GameObject lilBuddy;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

            switch (whichItem)
            {
                case WhichItem.HamShank:
                    if (GlobalController.Instance.currentHealth <= 16) GlobalController.Instance.currentHealth += 4;
                    else GlobalController.Instance.currentHealth = 20;
                    break;
                case WhichItem.JellyMagnet:
                    GlobalController.Instance.jellyMagnet = true;
                    break;
                case WhichItem.LilBuddy:
                    GlobalController.Instance.lilBuddy += 1;
                    Instantiate(lilBuddy, transform.position, transform.rotation);
                    break;
                case WhichItem.ObviousBomb:
                    GlobalController.Instance.obviousBomb = true;
                    break;
                case WhichItem.PanicOrb:
                    GlobalController.Instance.panicOrb = true;
                    break;
                case WhichItem.Slimenip:
                    GlobalController.Instance.slimenip = true;
                    break;
                case WhichItem.TinyAnvil:
                    GlobalController.Instance.tinyAnvil = true;
                    break;
                default:
                    GlobalController.Instance.TreasureMagnet = true;
                    break;
            }
            collision.gameObject.GetComponent<PlayerMovement>().UpdateText();
            transform.parent.parent.gameObject.SetActive(false);
        }
    }
}
