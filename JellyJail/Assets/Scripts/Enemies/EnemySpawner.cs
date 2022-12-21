using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject warden;
    [SerializeField] bool hasWarden = false;
    [SerializeField] float spawnRange;
    [SerializeField] int waveOneEnemyCount = 4;
    [SerializeField] int waveTwoEnemyCount = 6;
    [SerializeField] int waveThreeEnemyCount = 7;
    [SerializeField] float breakTime = 5f;
    [SerializeField] LayerMask groundLayer;
    public int currentWave = 0;
    public int totalEnemyCount = 0;
    bool takingABreak = false;
    int maxWaves = 3;
    public bool levelComplete = false;

    //enemy count:
    //Wave 1 = 3-4
    //Wave 2 = 5-6
    //Wave 3 = 6-7


    // Start is called before the first frame update
    void Start()
    {
        NewWave();
        if (hasWarden) maxWaves = 4;
        else maxWaves = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (totalEnemyCount <= 0 && currentWave < maxWaves && !takingABreak)
        {
            takingABreak = true;
            currentWave++;
            Invoke(nameof(NewWave), breakTime);
        }
        else levelComplete = true;
    }

    void NewWave()
    {
        int enemiesToSpawn = 0;
        switch (currentWave)
        {
            case 1:
                enemiesToSpawn = waveOneEnemyCount;
                break;
            case 2:
                enemiesToSpawn = waveTwoEnemyCount;
                break;
            case 3:
                enemiesToSpawn = waveThreeEnemyCount;
                break;
            case 4:
                enemiesToSpawn = 0;
                break;
            default:
                break;
        }

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }

        if (hasWarden && currentWave > 3)
        {
            Instantiate(warden, Vector3.zero, Quaternion.identity);
            //play some epic boss music
        }

        Debug.Log("Wave " + currentWave + ", spawning " + enemiesToSpawn + " enemies.");
        takingABreak = false;
    }

    void SpawnEnemy()
    {
        Vector3 randomLocation = new(Random.Range(-spawnRange, spawnRange), 1, Random.Range(-spawnRange, spawnRange));
        while (!Physics.Raycast(randomLocation, -transform.up, 2f, groundLayer))
        {
            //changing location if it's not on the ground
            randomLocation = new Vector3(Random.Range(-spawnRange, spawnRange), 1, Random.Range(-spawnRange, spawnRange));
        }
        Instantiate(enemies[Random.Range(0, enemies.Length)], randomLocation, Quaternion.identity);
        totalEnemyCount++;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new(spawnRange * 2, 10, spawnRange * 2));
    }
}