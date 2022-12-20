using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    [SerializeField] SphereCollider magnetCollider;
    private void Awake()
    {
        if (GlobalController.Instance.TreasureMagnet) magnetCollider.enabled = true;
        else magnetCollider.enabled = false;
    }
}
