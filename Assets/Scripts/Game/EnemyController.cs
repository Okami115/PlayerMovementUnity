using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private List<GameObject> listEnemies;


    [SerializeField] private Transform[] spawnPoints;
    //TODO: Fix - Unclear name
    //TODO: BUG - Change Type to EnemyMovement to be able to call GetComponent without nullExceptions
    [SerializeField] private GameObject Enemy;

    [SerializeField] private Transform target;

    //TODO: Fix - Unclear name
    [SerializeField] private int maxEnemiesSpawned = 1;

    [SerializeField] private float currentSpeed = 10;

    private int round = 1;

    //TODO: Fix - Unclear name
    public event System.Action swichScene;

    void Update()
    {

        for (int i = 0; i < listEnemies.Count; i++)
        {
            //TODO: Fix - Should be event based
            if (listEnemies[i].GetComponent<Enemy>().GetHealt() < 0)
            {
                //TODO: OOP - Objects should control themselves
                Destroy(listEnemies[i].gameObject);
                listEnemies.Remove(listEnemies[i]);
                maxEnemiesSpawned++;
            }
        }

        //TODO: Fix - Should be event based
        if (listEnemies.Count == 0)
        {
            //TODO: Fix - Hardcoded value
            currentSpeed = currentSpeed * (round / 2);

            //TODO: Fix - Hardcoded value
            //TODO: Fix - Mathf.Clamp
            if (currentSpeed > 50) { currentSpeed = 50;}

            //TODO: OOP - override      OR     TP2 - Strategy
            //TODO: Fix - Hardcoded value
            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial"))
            {
                //TODO: TP2 - SOLID
                swichScene?.Invoke();
            }
            else
            {
                for (int i = 0; i < maxEnemiesSpawned; i++)
                {
                    int rand = Random.Range(0, spawnPoints.Length);

                    listEnemies.Add(Instantiate(Enemy, spawnPoints[rand].transform.position, spawnPoints[rand].transform.rotation));
                    listEnemies[i].GetComponent<EnemyMovement>().SetTarget(target);
                }
            }
        }
    }

}
