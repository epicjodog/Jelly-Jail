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

    [Header("Shooting")]
    Camera mainCamera;
    public bool isShooting; //maybe i'll use this?
    [SerializeField] float timeBetweenShots;

    [SerializeField] GameObject bulletGO;
    [SerializeField] Transform firePoint;

    //[SerializeField] int maxBullets = 3; //max bullets before needing to pick them up again
    int bullets = 3; //number of bullets carried

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

        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveVelocity = moveInput * moveSpeed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //rb.velocity.y = 10f;
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && bullets >= 1)
        {
            GameObject newBullet = Instantiate(bulletGO, firePoint.position, firePoint.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = transform.forward * 10;
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            bullets--;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("picked up a slime ball");
            Destroy(collision.gameObject); //expensive but works
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            bullets++;
        }
    }

    void FixedUpdate()
    {
        //rb.velocity = moveVelocity;
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
        //Debug.Log(rb.velocity.y);
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
}
