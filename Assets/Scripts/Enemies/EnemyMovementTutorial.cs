using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementTutorial : MonoBehaviour
{
    [SerializeField] private Transform[] target;
    [SerializeField] private NavMeshAgent agent;

    private float minDistance = 5;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: Fix - Add [RequireComponentAttribute]
        agent = GetComponent<NavMeshAgent>();
        int rand = Random.Range(0, target.Length);
        agent.destination = target[rand].position;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Fix - myNavAgent.remainingDistance
        //TODO: Fix - Could be a coroutine where you calculate the arriving time and yield a waitForSeconds
        float distance = Vector3.Distance(transform.position, agent.destination);

        if (distance <= minDistance) 
        {
            int rand = Random.Range(0, target.Length);
            agent.destination = target[rand].position;
        }
    }

}
