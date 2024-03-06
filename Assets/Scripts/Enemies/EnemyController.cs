using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the amount of enemies that exist.
/// </summary>
public class EnemyController : MonoBehaviour
{

    [SerializeField] private List<GameObject> listEnemies;

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private GameObject[] enemiesPrefab;
    [SerializeField] private GameObject bossPrefab;

    [SerializeField] private Transform target;

    [SerializeField] private int enemySpawnLimit;
    [SerializeField] private int enemyCountRoundIncrement = 3;

    [SerializeField] private int roundInterval = 3;

    [SerializeField] private float currentSpeed = 10;
    [SerializeField] private float maxSpeed = 50;
    [SerializeField] private float SpeedMultiplier = 0.5f;

    public event System.Action<int> endRound;

    private int round;

    private void Start()
    {
        round = 1;

        for (int i = 0; i < listEnemies.Capacity; i++)
        {
            listEnemies[i].GetComponent<Health>().wasDefeated += DestroyEnemy;
        }

        for (int  i = 0;  i < enemySpawnLimit;  i++)
        {
            int rand = Random.Range(0, spawnPoints.Length);
            SpawnEnemy(spawnPoints[rand].transform.position, spawnPoints[rand].transform.rotation);
        }
    }

    /// <summary>
    /// Eliminate the enemies and spawn the new wave
    /// </summary>
    /// <param name="health"></param>
    private void DestroyEnemy(Health health)
    {
        listEnemies.Remove(health.gameObject);
        Destroy(health.gameObject);

        if (listEnemies.Count == 0)
        {
            round++;
            endRound?.Invoke(round);

            currentSpeed = currentSpeed * (round * SpeedMultiplier);
            enemySpawnLimit += enemyCountRoundIncrement;

            Mathf.Clamp(currentSpeed, 0, maxSpeed);

            for (int i = 0; i < enemySpawnLimit; i++)
            {
                int rand = Random.Range(0, spawnPoints.Length);
                SpawnEnemy(spawnPoints[rand].transform.position, spawnPoints[rand].transform.rotation);
            }
        }
    }

    /// <summary>
    /// Spawn new enemies
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    private void SpawnEnemy(Vector3 position, Quaternion rotation)
    {
        int rand = Random.Range(0, enemiesPrefab.Length);

        GameObject newEnemy = Instantiate(enemiesPrefab[rand], position, rotation);
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

    /// <summary>
    /// Eliminate all enemies in the round
    /// </summary>
    public void Nuke()
    {
        for (int i = listEnemies.Count - 1; i >= 0; i--)
        {
            DestroyEnemy(listEnemies[i].GetComponent<Health>());
        }
    }
}
