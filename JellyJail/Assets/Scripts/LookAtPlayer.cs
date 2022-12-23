using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerController");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        Quaternion newRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        transform.rotation = newRotation;
    }
}
