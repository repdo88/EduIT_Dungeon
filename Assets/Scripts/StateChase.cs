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
    [SerializeField] private AudioSource audioSource; // Reference to the AudioSource component
    [SerializeField] private AudioClip chaseSound; // Sound to play when chasing the player

    public override void OnEnterState()
    {
        base.OnEnterState();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed; // Set the speed of the agent
        agent.stoppingDistance = stoppingDistance; // Set the stopping distance for the agent
        audioSource.loop = true; // Set the audio source to loop
        audioSource.clip = chaseSound; // Assign the patrol sound to the audio source
        audioSource.Play(); // Play the patrol sound
    }

    public override void UpdateState()
    {
        agent.SetDestination(Player.position);
    }

    public override void OnExitState()
    {
        base.OnExitState();
        audioSource.Stop(); // Stop the patrol sound if audio source exists
        audioSource.loop = false; // Disable looping for the audio source
    }
}