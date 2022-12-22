using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// Player movement, aiming, and firing
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float moveSpeed;
    [SerializeField] float invulnerabilityPeriod = 2f;
    bool isInvulnerable;
    Rigidbody rb;
    Vector3 moveInput;
    Vector3 moveVelocity;
    public bool isInAir;
    [SerializeField] float timeBetweenJumps = 3f;
    bool isJumping = false;

    [Header("Shooting")]
    Camera mainCamera;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] float shootForce = 13;
    [SerializeField] GameObject bulletGO;
    [SerializeField] Transform firePoint;
    bool isShooting = false;

    [Header("Items")]
    public int bullets = 3; //number of bullets carried
    public int health = 4;
    public int tokens = 0;
    [SerializeField] GameObject buddyGO;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI tokenText;
    [SerializeField] TextMeshProUGUI waveText;
    AudioManager audioMan;
    EnemySpawner enemySpawner;
    bool isHealing = false;
    bool localLevelComplete;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        audioMan = GetComponent<AudioManager>();

        if(GlobalController.Instance.lilBuddy)
        {
            Instantiate(buddyGO, transform.position, transform.rotation);
        }

        health = GlobalController.Instance.currentHealth;
        bullets = GlobalController.Instance.maxBullets;
        tokens = GlobalController.Instance.tokens;
        if (GlobalController.Instance.panicOrb) invulnerabilityPeriod *= 2;

        healthText.text = health.ToString();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Move();
        waveText.text = enemySpawner.currentWave.ToString();

        if(Input.GetKeyDown(KeyCode.Mouse0) && bullets >= 1 && !isShooting)
        {
            //spawns a bullet and throws it
            GameObject newBullet = Instantiate(bulletGO, firePoint.position, firePoint.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = transform.forward * shootForce;
            //player gets smaller and removes a bullet from inventory
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            bullets--;
            isShooting = true;
            audioMan.Play("Shoot");
            Invoke(nameof(RechargeShoot), timeBetweenShots);
            ammoText.text = bullets.ToString();
        }
        if (enemySpawner.levelComplete) localLevelComplete = true; 
        if(localLevelComplete && !isHealing && health < 10)
        {
            isHealing = true;
            Invoke(nameof(Regenerate), 2f);
        }
    }
    void RechargeShoot() { isShooting = false; }
    void RechargeGroundpound() { isJumping = false; }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            //Debug.Log("picked up a slime ball");
            Destroy(collision.gameObject); //expensive but works
            //player gets bigger and adds a bullet to inventory
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            bullets++;
            audioMan.Play("Pickup");
            ammoText.text = bullets.ToString();
        }
        if(collision.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage();
        }
        if(collision.gameObject.CompareTag("Token"))
        {
            tokens++;
            Destroy(collision.gameObject);
            audioMan.Play("Token");
            tokenText.text = tokens.ToString();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            //player gets bigger and adds a bullet to inventory
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            bullets++;
        }
        if (other.gameObject.CompareTag("Token"))
        {
            tokens++;
            Destroy(other.gameObject);
            Debug.Log("picked up a token, " + tokens);
        }
        if (other.gameObject.CompareTag("EnemyBullet")) //Shockwave
        {
            TakeDamage();
            Destroy(other.transform.parent.gameObject);
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
        if (isInvulnerable) return;
        audioMan.Play("Hurt");
        health -= 1;
        healthText.text = health.ToString();
        isInvulnerable = true;
        Invoke(nameof(Recover), invulnerabilityPeriod);
        if(health <= 0)
        {
            Debug.LogWarning("Player has died");
        }
    }
    void Recover()
    {
        isInvulnerable = false;
    }
    void Regenerate()
    {
        health += 1;
        healthText.text = health.ToString();
        isHealing = false;
    }

    public void AddHealthAmmo(int healthAdded, int ammoAdded)
    {
        health += healthAdded;
        bullets += ammoAdded;
    }
    public void Save()
    {
        GlobalController.Instance.currentHealth = health;
        GlobalController.Instance.tokens = tokens;
    }
}
