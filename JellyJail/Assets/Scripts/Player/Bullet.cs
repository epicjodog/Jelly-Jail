using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeBeforePickup = 1f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
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
