using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private string sceneName = "Tutorial";
    [SerializeField] private List<GameObject> listEnemies;

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private Transform target;

    [SerializeField] private int enemySpawnLimit = 1;

    [SerializeField] private float currentSpeed = 10;
    [SerializeField] private float maxSpeed = 50;
    [SerializeField] private float SpeedMultiplier = 0.5f;

    private int round = 1;

    public event System.Action changeScene;

    private void DestroyEnemy(Health health)
    {
        enemySpawnLimit++;
        listEnemies.Remove(health.gameObject);
        Destroy(health.gameObject);

        if (listEnemies.Count == 0)
        {
            currentSpeed = currentSpeed * (round * SpeedMultiplier);

            Mathf.Clamp(currentSpeed, 0, maxSpeed);

            //TODO: OOP - override      OR     TP2 - Strategy
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(sceneName))
            {
                //TODO: TP2 - SOLID
                changeScene?.Invoke();
            }
            else
            {
                for (int i = 0; i < enemySpawnLimit; i++)
                {
                    // new spawner
                    int rand = Random.Range(0, spawnPoints.Length);
                    SpawnEnemy(spawnPoints[rand].transform.position, spawnPoints[rand].transform.rotation);
                }
            }
        }
    }

    private void SpawnEnemy(Vector3 position, Quaternion rotation)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, position, rotation);
        listEnemies.Add(newEnemy);
        if (newEnemy.TryGetComponent(out Health health))
        {
            health.wasDefeated += DestroyEnemy;
        }
        if (newEnemy.TryGetComponent(out EnemyMovement enemyMovement))
        {
            enemyMovement.Target = target;
        }
    }
}
