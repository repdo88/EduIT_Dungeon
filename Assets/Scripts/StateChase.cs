using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateChase : EnemyBaseState
{
    [SerializeField] GameObject Player; // Reference to the player GameObject
    private NavMeshAgent agent; // Reference to the NavMeshAgent component


    public override void OnEnterState()
    {
        base.OnEnterState();
        agent = GetComponent<NavMeshAgent>();
    }

    public override void UpdateState()
    {
        agent.SetDestination(Player.transform.position);
    }
}
