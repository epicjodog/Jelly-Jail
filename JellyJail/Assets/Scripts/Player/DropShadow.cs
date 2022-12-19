using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Follows the player and appears when they "jump"
/// </summary>
public class DropShadow : MonoBehaviour
{
    [SerializeField] GameObject playerGO;
    PlayerMovement playerController;
    MeshRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        playerController = playerGO.GetComponent<PlayerMovement>();
        rend = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.isInAir)
        {
            rend.enabled = true;
            transform.position = 
                    new Vector3(playerGO.transform.position.x, 
                    playerGO.transform.position.y - playerController.DistanceFromGround(), 
                    playerGO.transform.position.z);
        }
        else
        {
            rend.enabled = false;
        }
    }
}
