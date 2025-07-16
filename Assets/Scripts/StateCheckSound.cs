using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateCheckSound : EnemyBaseState
{
    [SerializeField] private float chaseSpeed = 2.0f; // Speed of the chase
    [SerializeField] private float stoppingDistance = 2f; // Distance at which the agent stops
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    

    public override void OnEnterState2(Vector3 doorPos)
    {
        base.OnEnterState2(doorPos);
        agent = GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed; // Set the speed of the agent
        agent.stoppingDistance = stoppingDistance; // Set the stopping distance for the agent
        agent.SetDestination(doorPos);


    }

    public override void UpdateState()
    {
        
        base.UpdateState();
        
    }

    public override void OnExitState()
    {
        base.OnExitState();
        
    }

}
