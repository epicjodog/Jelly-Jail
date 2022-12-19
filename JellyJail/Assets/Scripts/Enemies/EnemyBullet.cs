using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float timeBeforeRemove = 1f;
    float timer;

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Invoke(nameof(Remove), timeBeforeRemove);
    }

    void Remove()
    {
        Destroy(gameObject);
    }
}
