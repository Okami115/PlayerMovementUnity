using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This is a component to can move the enemy with NavMesh.
/// This can follow the target on the map
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float safeZone = 2;

    public Transform Target { get => target; set => target = value; }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = Target.position;
    }

    void Update()
    {
        agent.destination = Target.position;

        if(agent.remainingDistance > safeZone) 
        {
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }
    }
}
