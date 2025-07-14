
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class StatePatrol : EnemyBaseState
{
    [SerializeField] private float patrolSpeed = 1.0f; // Speed of the patrol
    [SerializeField] private float stoppingDistance = 0.1f; // Distance at which the agent stops
    [SerializeField] private float pauseTime = 1f; // Time to pause at each waypoint
    [SerializeField] private Transform[] waypoints; // Array of waypoints for the enemy to patrol
    private int currentWaypointIndex = 0; // Index of the current waypoint
    private Transform currentWaypoint; // Current waypoint transform
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    
    private float pauseTimer = 0f; // Timer to track pause duration
    public override void OnEnterState()
    {
        base.OnEnterState();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed; // Set the speed of the agent
        agent.stoppingDistance = stoppingDistance; // Set the stopping distance for the agent
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
            pauseTimer += Time.deltaTime; // Increment the pause timer
            if (pauseTimer >= pauseTime)
            {
                pauseTimer = 0f; // Reset the pause timer
                GoNextPoint(); // Move to the next waypoint
            }
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
