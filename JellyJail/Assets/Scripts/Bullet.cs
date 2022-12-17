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
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > timeBeforePickup)
        {
            gameObject.tag = "Bullet";
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
