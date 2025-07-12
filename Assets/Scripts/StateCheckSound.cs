using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateCheckSound : EnemyBaseState
{
    [SerializeField] private float chaseSpeed = 2.0f; // Speed of the chase
    [SerializeField] private float stoppingDistance = 2f; // Distance at which the agent stops
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    //private Vector3 destination; // Destination for the agent

    public override void OnEnterState(Vector3 doorPos)
    {
        base.OnEnterState();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed; // Set the speed of the agent
        agent.stoppingDistance = stoppingDistance; // Set the stopping distance for the agent
        agent.SetDestination(doorPos);
        //OpenCloseDoor.OnDoorInteract += DeterminateDestination; // Subscribe to the door interaction event
    }

    public override void UpdateState()
    {
        
        base.UpdateState();
        
    }

    public override void OnExitState()
    {
        base.OnExitState();
        //OpenCloseDoor.OnDoorInteract -= DeterminateDestination; // Unsubscribe from the door interaction event
    }

    //public void DeterminateDestination(OpenCloseDoor door)
    //{
        
    //    destination = door.transform.position;
    //    agent.SetDestination(destination);
    //    Debug.Log("Destination set to door position: " + destination);
    //}
}
