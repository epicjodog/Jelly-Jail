using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeBeforePickup = 1f;
    [SerializeField] SphereCollider magnetCollider;
    [SerializeField] GameObject bomb;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        if (GlobalController.Instance.jellyMagnet) magnetCollider.enabled = true;
        else magnetCollider.enabled = false;
        if (GlobalController.Instance.obviousBomb) bomb.SetActive(true);
        else bomb.SetActive(false);

        gameObject.tag = "Untagged";
        gameObject.layer = 7;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > timeBeforePickup)
        {
            gameObject.tag = "Bullet";
            gameObject.layer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
