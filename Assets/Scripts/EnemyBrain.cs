using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    [Header("Sensors")]
    [SerializeField] private float visionAngleThreshold = 0.3f;
    private float tempVisionAngleTheshold;
    [SerializeField] private float visionMaxDistance = 3f;
    [SerializeField] private float minRadius = 3f;
    [SerializeField] private bool seePlayer;
    [SerializeField] private bool inAngleVision;
    [SerializeField] private bool inRangeVision;
    [SerializeField] private bool inMinRadius;
    private Vector3 directionToPlayer;
    private Vector3 directionToChest;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private bool isActive = true; // Flag to enable or disable the enemy sensors

    [Header("State")]
    [SerializeField] private EnemyBaseState defaultState; // Default state of the enemy

    [SerializeField] private EnemyBaseState patrolState; // Patrol state of the enemy
    [SerializeField] private EnemyBaseState chaseState; // Chase state of the enemy

    private EnemyBaseState currentState; // Current state of the enemy

    [Header("Player")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerChest;
    [SerializeField] private NavMeshAgent agent;

    private ThirdPersonController playerController;
    [SerializeField] private bool playerIsCrouch;
    [SerializeField] private float playerSpeed;
    [SerializeField] private string playerLayer = "Player";

    // Start is called before the first frame update
    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        playerController = ThirdPersonController.Instance;

        currentState = defaultState;
        currentState.OnEnterState(); // Initialize the current state
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            currentState.UpdateState(); // Update the current state of the enemy
        }
        UpdateSensors();
    }

    private void UpdateSensors()
    {
        //logic to simulate that the enemy can hear the player when is moving and is not crouch
        playerIsCrouch = playerController.playerIsCrouch;
        playerSpeed = playerController.playerSpeed;
        if (!playerIsCrouch && playerSpeed > 0f)
        {
            tempVisionAngleTheshold = 2f;
        }
        else
        {
            tempVisionAngleTheshold = visionAngleThreshold;
        }
        //calculate distance to player
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        //calculate direction to player
        directionToPlayer = (player.transform.position - transform.position).normalized;
        directionToChest = (playerChest.transform.position - transform.position).normalized;
        //calculate angle to player
        float angleToPlayer = Vector3.Dot(transform.forward, directionToPlayer);
        //bools to see the state of the sensors
        inAngleVision = angleToPlayer > (1 - tempVisionAngleTheshold);
        inRangeVision = distanceToPlayer < visionMaxDistance;
        inMinRadius = distanceToPlayer < minRadius;
        

        if ((inAngleVision && inRangeVision) || inMinRadius)
        {
            Ray vision = new Ray(transform.position, directionToChest);
            if (Physics.Raycast(vision, out RaycastHit hitInfo, visionMaxDistance))
            {
                int targetLayer = LayerMask.NameToLayer(playerLayer);
                seePlayer = (hitInfo.collider.gameObject.layer == targetLayer);
            }
            else
            {
                seePlayer = false;
            }
        }
        else
        {
            seePlayer = false;
        }


    }

    private void OnDrawGizmosSelected()
    {
        if (seePlayer)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawRay(transform.position, directionToChest.normalized * visionMaxDistance);
    }

}
