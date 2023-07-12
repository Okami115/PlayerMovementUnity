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

    /// <summary>
    /// Execute the movement of the enemy within the tutorial
    /// </summary>
    /// <returns></returns>
    private IEnumerator Move()
    {
        while (gameObject.activeSelf) 
        {
            yield return new WaitUntil(IsOnTarget);

            int rand = Random.Range(0, target.Length);
            agent.destination = target[rand].position;
        }
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Move());
    }

    private bool IsOnTarget()
    {
        if (agent == null)
            return false;
        else
            return (agent.remainingDistance <= minDistance);
    }

    private void OnDestroy()
    {
        StopCoroutine(Move());
    }
}
