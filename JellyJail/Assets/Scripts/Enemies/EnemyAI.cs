using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    enum EnemyType
    {
        Angry,
        Spiky,
        Splitter,
        Shielded,
        Ranger,
        Warden
    }

    [Header("General")]
    [SerializeField] EnemyType enemyType = EnemyType.Angry;
    [SerializeField] int health = 3;
    [SerializeField] TextMeshPro healthText;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject player;
    [SerializeField] LayerMask groundLayer, playerLayer;
    [SerializeField] Vector2 numberOfTokensOnDeath = new (1, 2); //min/max
    [SerializeField] GameObject tokenGO;
    [SerializeField] float wakeUpTime = 2;
    EnemySpawner enemySpawner;
    AudioManager audioMan;
    Animator enemyAnim;

    [Header("Patrolling")]  
    [SerializeField] float walkPointRange;
    Vector3 walkPoint;
    bool walkPointSet;

    [Header("Attacking")]
    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked;

    [Header("AI States")]
    [SerializeField] float sightRange;
    [SerializeField] float attackRange;
    bool playerInSightRange, playerInAttackRange;

    [Header("Shooting")]
    [SerializeField] GameObject bulletGO;
    [SerializeField] Transform firePoint;
    [SerializeField] float shootForce = 13;

    [Header("Warden")]
    [SerializeField] GameObject shockWaveGO;
    float timer; //just for the warden
    bool makingShockwaves = false;
    bool isInAir = false;
    bool firstShockwave = true;
    Quaternion originalRotation;

    private void Awake()
    {
        player = GameObject.Find("PlayerController");
        agent = GetComponent<NavMeshAgent>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        audioMan = GetComponent<AudioManager>();
        enemyAnim = GetComponent<Animator>();
    }
    private void Start()
    {
        healthText.text = health.ToString();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (health <= 0) return;

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (enemyType == EnemyType.Angry)
        {
            if (!playerInSightRange && !playerInAttackRange) Patrolling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
        else if (enemyType == EnemyType.Ranger)
        {
            if (!playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && !playerInAttackRange) RunAway();
            if (playerInSightRange && playerInAttackRange) { ShootPlayer(); RunAway(); }
        }
        else if (enemyType == EnemyType.Shielded)
        {
            if (!playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && !playerInAttackRange) RunAway();
            if (playerInSightRange && playerInAttackRange) BashPlayer();
            LookAtPlayer();
        }
        else if (enemyType == EnemyType.Spiky)
        {
            if (!playerInSightRange && !playerInAttackRange) Patrolling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) ShootPlayer();

        }
        else if (enemyType == EnemyType.Splitter)
        {
            if (!playerInSightRange && !playerInAttackRange) Patrolling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
        else if (enemyType == EnemyType.Warden)
        {
            
            //Debug.Log(timer);
            if (timer >= 20f)
            {
                if (!makingShockwaves) makingShockwaves = true;
                else makingShockwaves = false;
                timer = 0f;
            }

            if (!makingShockwaves)
            {
                agent.enabled = true;
                ChasePlayer();
                ShootPlayer();
                firstShockwave = true;
            }
            else
            {
                agent.enabled = false;
                originalRotation = new Quaternion(0, transform.rotation.y, 0, 0);
                Shockwave();
            }
            
        }
    }

    void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 2f) walkPointSet = false;
    }
    void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }
    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        LookAtPlayer();
        //preventing enemies from immediately attacking the player after spawning 
        if (wakeUpTime > timer) return;

        if (!alreadyAttacked)
        {
            Debug.Log("Attacked Player");
            enemyAnim.SetTrigger("Attack");
            player.GetComponent<PlayerMovement>().TakeDamage();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    void RunAway()
    {
        Vector3 runTo = transform.position + ((transform.position - player.transform.position)/* * multiplier*/);
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < sightRange) agent.SetDestination(runTo);
    }
    void ShootPlayer()
    {
        LookAtPlayer();
        //preventing enemies from immediately shooting the player after spawning 
        if (wakeUpTime > timer) return;

        if (!alreadyAttacked)
        {
            audioMan.Play("Shoot");
            enemyAnim.SetTrigger("Attack");
            GameObject newBullet = Instantiate(bulletGO, firePoint.position, firePoint.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = transform.forward * shootForce;
            //player.GetComponent<PlayerMovement>().TakeDamage();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    void BashPlayer()
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        Debug.Log(transform.forward);
        rb.AddForce(transform.forward * 500);
    }
    void Shockwave()
    {
        
        Rigidbody rb = GetComponent<Rigidbody>();
        isInAir = !Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.1f);
        if (!isInAir)
        {
            enemyAnim.SetTrigger("Special");
            rb.AddForce(new Vector3(0, 50, 0));
            isInAir = true;
            if(!firstShockwave)
            {
                Instantiate(shockWaveGO, transform.position, Quaternion.identity);
                transform.rotation = originalRotation;
                audioMan.Play("Shockwave");
            }
            firstShockwave = false;
        }
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer)) walkPointSet = true;
    }
    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (health < 1) return;
        if (collision.gameObject.layer == 7) //layer 7: bullets
        {
            TakeDamage(1);
        }      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (health < 1) return;
        if (other.gameObject.CompareTag("Groundpound") && enemyType != EnemyType.Splitter)
        {
            if(GlobalController.Instance.tinyAnvil) TakeDamage(Random.Range(3, 6));
            else TakeDamage(Random.Range(2, 4));
            audioMan.Play("Groundpound");
            //uuuuuuuuuuu
            Rigidbody rb = other.gameObject.GetComponentInParent<Rigidbody>();
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
            if(enemyType == EnemyType.Spiky)
            {
                other.gameObject.GetComponentInParent<PlayerMovement>().TakeDamage();
            }
        }
        if (other.gameObject.layer == 7 && other.gameObject.CompareTag("Bomb")) //layer 7: bullets
        {
            //only if it's specifically the bomb shockwave
            TakeDamage(1);
        }
    }

    void TakeDamage(int damageAmount)
    {
        audioMan.Play("Hurt");
        health -= damageAmount;
        if (health > 0) healthText.text = health.ToString();
        if (health <= 0)
        {
            audioMan.Play("Death");
            healthText.text = "0";
            agent.enabled = false;
            Invoke(nameof(Die), 0.5f); //delayed time before dying, maybe we need it?
                                       //death animation          
        }
    }

    void Die()
    {
        if (enemyType != EnemyType.Splitter) enemySpawner.totalEnemyCount--;
        else enemySpawner.totalEnemyCount++; //compensating for the splitter adding 2 new enemies on death
        for (int i = 0; i < Random.Range(numberOfTokensOnDeath.x, numberOfTokensOnDeath.y); i++)
        {
            //Vector3 randomSpawn = new Vector3(transform.position.x + Random.Range(0.25f, 0.6f),
                //transform.position.y, transform.position.z + Random.Range(0.25f, 0.6f));
            GameObject newToken = Instantiate(tokenGO, transform.position, transform.rotation);
            newToken.GetComponent<Rigidbody>().velocity = transform.up * 8;
        }

        Destroy(gameObject);

        if (enemyType == EnemyType.Splitter)
        {
            Instantiate(bulletGO, (transform.position + Vector3.left), transform.rotation);
            Instantiate(bulletGO, (transform.position + Vector3.right), transform.rotation);
        }
    }
    void LookAtPlayer()
    {
        transform.LookAt(player.transform);
        Quaternion newRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        transform.rotation = newRotation;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new(walkPointRange * 2, 10, walkPointRange * 2));
    }
}
