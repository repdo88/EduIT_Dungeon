using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateCheckSound : EnemyBaseState
{
    [SerializeField] private float chaseSpeed = 2.0f; // Speed of the chase
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    private Vector3 destination; // Destination for the agent

    public override void OnEnterState()
    {
        base.OnEnterState();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed; // Set the speed of the agent
        OpenCloseDoor.OnDoorInteract += SetDestination; // Subscribe to the door interaction event
    }

    public override void UpdateState()
    {
        base.UpdateState();
        agent.SetDestination(destination);
    }

    public override void OnExitState()
    {
        base.OnExitState();
        OpenCloseDoor.OnDoorInteract -= SetDestination; // Unsubscribe from the door interaction event
    }

    public void SetDestination(OpenCloseDoor door)
    {
        destination = door.transform.position;
        
    }
}
