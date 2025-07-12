using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateChase : EnemyBaseState
{
    [SerializeField] private float chaseSpeed = 3.0f; // Speed of the chase
    [SerializeField] GameObject Player; // Reference to the player GameObject
    private NavMeshAgent agent; // Reference to the NavMeshAgent component


    public override void OnEnterState()
    {
        base.OnEnterState();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed; // Set the speed of the agent
    }

    public override void UpdateState()
    {
        agent.SetDestination(Player.transform.position);
    }
}
