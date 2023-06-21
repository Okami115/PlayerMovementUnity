using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent; 

    void Start()
    {
        //TODO: Fix - Add [RequireComponentAttribute]
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = target.position;
        
    }

    //TODO: Fix - Should be native Setter/Getter
    public Transform GetTarget()
    {
        return target;
    }

    //TODO: Fix - Should be native Setter/Getter
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
