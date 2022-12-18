using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Player movement, aiming, and firing
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float moveSpeed;
    Rigidbody rb;
    Vector3 moveInput;
    Vector3 moveVelocity;
    public bool isInAir;
    [SerializeField] float timeBetweenJumps = 3f;
    bool isJumping = false;

    [Header("Shooting")]
    Camera mainCamera;
    //public bool isShooting; //maybe i'll use this?
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] float shootForce = 13;
    [SerializeField] GameObject bulletGO;
    [SerializeField] Transform firePoint;
    bool isShooting = false;

    public int bullets = 3; //number of bullets carried
    public int health = 4;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Move();

        if(Input.GetKeyDown(KeyCode.Mouse0) && bullets >= 1 && !isShooting)
        {
            //spawns a bullet and throws it
            GameObject newBullet = Instantiate(bulletGO, firePoint.position, firePoint.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = transform.forward * shootForce;
            //player gets smaller and removes a bullet from inventory
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            bullets--;
            isShooting = true;
            Invoke(nameof(RechargeShoot), timeBetweenShots);
        }
        
    }
    void RechargeShoot() { isShooting = false; }
    void RechargeGroundpound() { isJumping = false; }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("picked up a slime ball");
            Destroy(collision.gameObject); //expensive but works
            //player gets bigger and adds a bullet to inventory
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            bullets++;
        }
    }

    void FixedUpdate()
    {
        //rb.velocity = moveVelocity;
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
        //Debug.Log(rb.velocity);
    }

    (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
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

    void Move()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.1f))
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                moveVelocity.y = 10;
                rb.velocity = new Vector3(rb.velocity.x, moveVelocity.y, rb.velocity.z);
                isJumping = true;
                Invoke(nameof(RechargeGroundpound), timeBetweenJumps);
            }
            isInAir = false;
        }
        else
        {
            isInAir = true;
        }
        
        //Debug.Log(moveVelocity);
    }

    void Aim()
    {
        var (success, position) = GetMousePosition();
        if(success)
        {
            var direction = position - transform.position;
            direction.y = 0f;
            transform.forward = direction;
        }
    }

    public float DistanceFromGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            return hit.distance;
        }
        else
        {
            return 0f;
        }
    }

    public void TakeDamage()
    {
        Debug.LogWarning("Player has taken damage");
        health -= 1;
        //update text
        if(health <= 0)
        {
            Debug.LogWarning("Player has died");
        }
    }
}
