using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private List<GameObject> listEnemies;


    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject Enemy;

    [SerializeField] private Transform target;

    [SerializeField] private int maxEnemiesSpawned = 1;

    [SerializeField] private float currentSpeed = 10;
    [SerializeField] private int currentHealt;

    private int round = 1;

    public event System.Action swichScene;

    void Start()
    {

    }


    void Update()
    {

        for (int i = 0; i < listEnemies.Count; i++)
        {
            if (listEnemies[i].GetComponent<Enemy>().GetHealt() < 0)
            {
                Destroy(listEnemies[i].gameObject);
                listEnemies.Remove(listEnemies[i]);
                maxEnemiesSpawned++;
            }
        }

        if (listEnemies.Count == 0)
        {
            currentSpeed = currentSpeed * (round / 2);

            if (currentSpeed > 50) { currentSpeed = 50;}

            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial"))
            {
                swichScene?.Invoke();
            }
            else
            {
                for (int i = 0; i < maxEnemiesSpawned; i++)
                {
                    int rand = Random.Range(0, spawnPoints.Length);

                    listEnemies.Add(Instantiate(Enemy, spawnPoints[rand].transform));
                    listEnemies[i].GetComponent<EnemyMovement>().SetTarget(target);
                }
            }
        }
    }

}
