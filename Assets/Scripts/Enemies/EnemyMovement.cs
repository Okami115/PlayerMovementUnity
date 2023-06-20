using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: Fix - Add [RequireComponentAttribute]
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
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
