using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateChase : EnemyBaseState
{
    [SerializeField] private float chaseSpeed = 3.0f; // Speed of the chase
    [SerializeField] private float stoppingDistance = 0.1f; // Distance at which the agent stops
    [SerializeField] private Transform Player; // Reference to the player GameObject
    private NavMeshAgent agent; // Reference to the NavMeshAgent component


    public override void OnEnterState()
    {
        base.OnEnterState();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed; // Set the speed of the agent
        agent.stoppingDistance = stoppingDistance; // Set the stopping distance for the agent
    }

    public override void UpdateState()
    {
        agent.SetDestination(Player.position);
    }
}
