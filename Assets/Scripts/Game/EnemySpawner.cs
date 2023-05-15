using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private List<GameObject> listEnemies;

    [SerializeField] private Transform target;

    private int maxEnemiesSpawned = 1;
    private int currentEnemies = 0;

    void Start()
    {
        enemyController.dead += isDead;
    }


    void Update()
    {
        if(currentEnemies == 0)
        { 
            for(int i = 0; i < maxEnemiesSpawned; i++) 
            { 
                int rand = Random.Range(0, spawnPoints.Length);

                listEnemies.Add(Instantiate(Enemy, spawnPoints[rand].transform));
                listEnemies[i].GetComponent<EnemyMovement>().SetTarget(target);
                currentEnemies++;
            }
        }
    }

    private void isDead(int index)
    {
        maxEnemiesSpawned++;
        currentEnemies--;
        listEnemies.Remove(listEnemies[index]);
    }
}
