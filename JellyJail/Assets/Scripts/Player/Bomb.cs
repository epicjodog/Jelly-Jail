using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject bombShockwave;
    AudioManager audioMan;
    bool isExploded = false;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        bombShockwave.SetActive(false);
        audioMan = GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= 2)
        {
            return;
        }

        if (!isExploded)
        {
            audioMan.PlayForceEntirely("Bomb");
            bombShockwave.SetActive(true);
            bombShockwave.transform.localScale =
            new Vector3(bombShockwave.transform.localScale.x + (3 * Time.deltaTime),
            bombShockwave.transform.localScale.y + (3 * Time.deltaTime),
            bombShockwave.transform.localScale.z + (3 * Time.deltaTime));

            if (bombShockwave.transform.localScale.x >= 5f)
            {
                Destroy(bombShockwave);
                isExploded = true;
            }
        }     

        if(isExploded)
        {
            transform.localScale =
            new Vector3(transform.localScale.x - (3 * Time.deltaTime),
            transform.localScale.y - (3 * Time.deltaTime),
            transform.localScale.z - (3 * Time.deltaTime));

            if (transform.localScale.x <= 0) gameObject.SetActive(false);
        }
    }
}
