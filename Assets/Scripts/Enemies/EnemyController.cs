using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private List<GameObject> listEnemies;


    [SerializeField] private Transform[] spawnPoints;

    //TODO: BUG - Change Type to EnemyMovement to be able to call GetComponent without nullExceptions
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private Transform target;

    [SerializeField] private int enemySpawnLimit = 1;

    [SerializeField] private float currentSpeed = 10;
    [SerializeField] private float maxSpeed = 50;
    [SerializeField] private float SpeedMultiplier = 0.5f;

    private int round = 1;

    public event System.Action changeScene;

    void Update()
    {

        for (int i = 0; i < listEnemies.Count; i++)
        {
            //TODO: Fix - Should be event based
            if (listEnemies[i].GetComponent<Health>().HPoints < 0)
            {
                //TODO: OOP - Objects should control themselves
                Destroy(listEnemies[i].gameObject);
                listEnemies.Remove(listEnemies[i]);
                enemySpawnLimit++;
            }
        }

        //TODO: Fix - Should be event based
        if (listEnemies.Count == 0)
        {
            currentSpeed = currentSpeed * (round * SpeedMultiplier);

            Mathf.Clamp(currentSpeed, 0, maxSpeed);

            //TODO: OOP - override      OR     TP2 - Strategy
            //TODO: Fix - Hardcoded value
            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial"))
            {
                //TODO: TP2 - SOLID
                changeScene?.Invoke();
            }
            else
            {
                for (int i = 0; i < enemySpawnLimit; i++)
                {
                    int rand = Random.Range(0, spawnPoints.Length);

                    listEnemies.Add(Instantiate(enemyPrefab, spawnPoints[rand].transform.position, spawnPoints[rand].transform.rotation));
                    listEnemies[i].GetComponent<EnemyMovement>().Target = target;
                }
            }
        }
    }

}
