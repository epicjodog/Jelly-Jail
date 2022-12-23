using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndItemChoice : MonoBehaviour
{
    [SerializeField] GameObject[] listOfItems;

    [SerializeField] Transform choiceOne;
    [SerializeField] Transform choiceTwo;
    [SerializeField] Transform choiceThree;

    int chosenItemOne;
    int chosenItemTwo;
    int chosenItemThree;

    EnemySpawner enemySpawner;
    bool localLevelComplete;
    bool choiceComplete;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }
    private void Update()
    {
        if (choiceComplete) return;
        if (enemySpawner.levelComplete) localLevelComplete = true;
        if(localLevelComplete)
        {
            SpawnChoices();
            choiceComplete = true;
        }
    }

    void SpawnChoices()
    {
        chosenItemOne = Random.Range(0, listOfItems.Length - 1);
        chosenItemTwo = Random.Range(0, listOfItems.Length - 1);
        while (chosenItemTwo == chosenItemOne)
        {
            chosenItemTwo = Random.Range(0, listOfItems.Length - 1);
        }
        chosenItemThree = Random.Range(0, listOfItems.Length - 1);
        while (chosenItemThree == chosenItemOne || chosenItemThree == chosenItemTwo)
        {
            chosenItemThree = Random.Range(0, listOfItems.Length - 1);
        }

        GameObject spawnedItem1 = Instantiate(listOfItems[chosenItemOne], choiceOne.position, Quaternion.identity);
        spawnedItem1.transform.parent = choiceOne.transform;
        GameObject spawnedItem2 = Instantiate(listOfItems[chosenItemTwo], choiceTwo.position, Quaternion.identity);
        spawnedItem2.transform.parent = choiceTwo.transform;
        GameObject spawnedItem3 = Instantiate(listOfItems[chosenItemThree], choiceThree.position, Quaternion.identity);
        spawnedItem3.transform.parent = choiceThree.transform;
    }
}
