using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This is a component to can move the enemy with NavMesh in the tutorial.
/// It is used to move the enemy towards random target and changes the target when it reaches the objetive. 
/// </summary> 



[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovementTutorial : MonoBehaviour
{
    [SerializeField] private Transform[] target;
    [SerializeField] private NavMeshAgent agent;
    

    private float minDistance = 5;
    IEnumerator Move()
    {
        if (agent.remainingDistance <= minDistance) 
        {
            int rand = Random.Range(0, target.Length);
            agent.destination = target[rand].position;
            yield return new WaitForSeconds(1f);
        }

    }



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        int rand = Random.Range(0, target.Length);
        agent.destination = target[rand].position;
        StartCoroutine(Move());
    }

    void Update()
    {
        //TODO: Fix - Could be a coroutine where you calculate the arriving time and yield a waitForSeconds
    }
}
