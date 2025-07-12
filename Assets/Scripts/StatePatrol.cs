using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class StatePatrol : EnemyBaseState
{
    [SerializeField] private Transform[] waypoints; // Array of waypoints for the enemy to patrol
    private int currentWaypointIndex = 0; // Index of the current waypoint
    private Transform currentWaypoint; // Current waypoint transform
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    public override void OnEnterState()
    {
        base.OnEnterState();
        agent = GetComponent<NavMeshAgent>();
        currentWaypoint = waypoints[(int)currentWaypointIndex]; // Get the next waypoint
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Trip();
    }
    private void Trip()
    {
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            GoNextPoint();
        }
        agent.SetDestination(currentWaypoint.position); // Set the agent's destination to the current waypoint
    }

    private void GoNextPoint()
    {
        if (currentWaypointIndex < waypoints.Count() - 1)
        {
            currentWaypointIndex++;
        }
        else
        {
            currentWaypointIndex = 0; // Loop back to the first waypoint
        }
        currentWaypoint = waypoints[(int)currentWaypointIndex]; // Get the next waypoint
    }

    public override void OnExitState()
    {
        base.OnExitState();
        Debug.Log("Exiting Patrol State");
    }
}
