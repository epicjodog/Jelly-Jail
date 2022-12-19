using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuddyController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject player;
    [SerializeField] LayerMask groundMask;

    [Header("Shooting")]
    Camera mainCamera;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] float shootForce = 13;
    [SerializeField] GameObject bulletGO;
    [SerializeField] Transform firePoint;
    bool isShooting = false;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("PlayerController");
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        agent.SetDestination(player.transform.position);

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isShooting)
        {
            //spawns a bullet and throws it
            GameObject newBullet = Instantiate(bulletGO, firePoint.position, firePoint.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = transform.forward * shootForce + transform.up;
            isShooting = true;
            Invoke(nameof(RechargeShoot), timeBetweenShots);
        }
    }

    void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            var direction = position - transform.position;
            direction.y = 0f;
            transform.forward = direction;
        }
    }
    (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            //raycast hit something where the mouse is
            return (success: true, position: hitInfo.point);
        }
        else
        {
            //raycast didn't hit anything where the mouse is
            return (success: false, position: Vector3.zero);
        }
    }
    void RechargeShoot() { isShooting = false; }

}
