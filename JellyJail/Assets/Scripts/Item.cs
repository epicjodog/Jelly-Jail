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

            if(whichItem == WhichItem.HamShank)
            {
                if (player.health <= 16) GlobalController.Instance.currentHealth += 4;
                else GlobalController.Instance.currentHealth = 20;
            }
            else if (whichItem == WhichItem.JellyMagnet)
            {
                GlobalController.Instance.jellyMagnet = true;
            }
            else if (whichItem == WhichItem.LilBuddy)
            {
                GlobalController.Instance.lilBuddy = true;
                Instantiate(lilBuddy, transform.position, transform.rotation);
            }
            else if (whichItem == WhichItem.ObviousBomb)
            {
                GlobalController.Instance.obviousBomb = true;
            }
            else if (whichItem == WhichItem.PanicOrb)
            {
                GlobalController.Instance.panicOrb = true;
            }
            else if (whichItem == WhichItem.Slimenip)
            {
                GlobalController.Instance.slimenip = true;
            }
            else if (whichItem == WhichItem.TinyAnvil)
            {
                GlobalController.Instance.tinyAnvil = true;
            }
            else
            {
                GlobalController.Instance.TreasureMagnet = true;
            }
            transform.parent.parent.gameObject.SetActive(false);
        }
    }
}
