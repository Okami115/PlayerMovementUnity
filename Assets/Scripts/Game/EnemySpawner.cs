using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private List<GameObject> listEnemies;

    [SerializeField] private Transform target;

    private int maxEnemiesSpawned = 1;
    private int currentEnemies = 0;

    void Start()
    {
        
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

        for (int i = 0; i < listEnemies.Count; i++)
        {
            if (listEnemies[i].GetComponent<Enemy>().GetHealt() < 0)
            {
                maxEnemiesSpawned++;
                Destroy(listEnemies[i].gameObject);
                listEnemies.Remove(listEnemies[i]);
                currentEnemies--;
            }
        }
    }
}
