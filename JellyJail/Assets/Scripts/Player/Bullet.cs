using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeBeforePickup = 1f;
    [SerializeField] SphereCollider magnetCollider;
    [SerializeField] GameObject bomb;
    GameObject player;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerController");
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
            //gameObject.layer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
        if(Vector3.Distance(gameObject.transform.position, player.transform.position) > 25)
        {
            player.GetComponent<PlayerMovement>().bullets++;
            Destroy(gameObject);
            player.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
}
