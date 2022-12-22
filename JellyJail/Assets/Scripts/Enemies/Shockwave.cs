using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.01f, transform.localScale.y, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale =
            new Vector3(transform.localScale.x + (5 * Time.deltaTime), 
            transform.localScale.y, 
            transform.localScale.z + (5 * Time.deltaTime));
        if(transform.localScale.x >= 20f)
        {
            Destroy(gameObject);
        }
    }
}
