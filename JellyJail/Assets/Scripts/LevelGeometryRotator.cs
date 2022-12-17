using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Rotates the GO by "rotationAmount".
///  Attach to an empty GO containing all level geometry to produce an Isometric effect
/// </summary>
public class LevelGeometryRotator : MonoBehaviour
{
    [SerializeField] Quaternion rotationAmount;

    void Start()
    {
        transform.rotation = rotationAmount;
    }
}
