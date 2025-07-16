using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2Brain : MonoBehaviour
{
    [Header("Sensors")]
    [SerializeField] private float visionAngleThreshold = 0.5f;
    private float tempVisionAngleTheshold;
    [SerializeField] private float visionMaxDistance = 3f;
    private bool seePlayer;
    private bool inAngleVision;
    private bool inRangeVision;
    private Vector3 directionToPlayer;
    private Vector3 directionToChest;
     private float distanceToPlayer;
     private bool isActive = true; // Flag to enable or disable the enemy sensors

    [Header("State")]
    [SerializeField] private EnemyBaseState defaultState; // Default state of the enemy

    [SerializeField] private EnemyBaseState patrolState; // Patrol state of the enemy
    [SerializeField] private float patrolTime = 5f; // Time to wait before patrolling again
    [SerializeField] private EnemyBaseState chaseState; // Chase state of the enemy
    [SerializeField] private EnemyBaseState checkSoundState; // Chase state of the enemy
    private bool doorTrigger = false; // Flag to check if the door is triggered
    private float distanceToDoor; // Distance to the door when triggered
    private EnemyBaseState currentState; // Current state of the enemy
    private float lostPlayerTimer = 0f; // Timer to track how long the player has been lost
    private Vector3 doorPosition; // Position of the door when triggered
    [SerializeField] private EnemyBaseState attackState; // Attack state of the enemy

    [Header("Player")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerChest;
    [SerializeField] private NavMeshAgent agent;

    private ThirdPersonController playerController;
    private bool playerIsCrouch;
    private float playerSpeed;
    [SerializeField] private string playerLayer = "Player";

    private int animIDSpeed; // Animator ID for speed parameter
    private Animator animator; // Animator component for the enemy
    private bool hasAnimator; // Flag to check if the enemy has an animator component

    private void OnEnable()
    {
        OpenCloseDoor.OnDoorInteract += OnDoorEvent; // Subscribe to the door interaction event
    }

    private void OnDisable()
    {
        OpenCloseDoor.OnDoorInteract -= OnDoorEvent; // Unsubscribe from the door interaction event
    }

    private void Awake()
    {
        hasAnimator = TryGetComponent<Animator>(out animator); // Check if the enemy has an animator component
        animIDSpeed = Animator.StringToHash("Speed"); // Initialize the animator ID for speed parameter
    }

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
        if (hasAnimator)
        {
            animator.SetFloat(animIDSpeed, agent.velocity.magnitude); // Update the animator speed parameter
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
        


        if ((inAngleVision && inRangeVision))
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

        // State transitions based on sensors
        if (!seePlayer && doorTrigger)
        {
            TransitionToState(checkSoundState); // Transition to check sound state if the door is triggered
            distanceToDoor = Vector3.Distance(transform.position, doorPosition);
            if (distanceToDoor < 3f)
            {
                doorTrigger = false; // Reset the door trigger flag if close to the door
                //TransitionToState(patrolState); // Transition to patrol state after a certain time
            }
            //doorTrigger = false; // Reset the door trigger flag
        }
        else if (seePlayer)
        {
            lostPlayerTimer = 0f; // Reset the timer if the player is seen
            TransitionToState(chaseState);
            doorTrigger = false; // Reset the door trigger flag
        }
        else
        {
            lostPlayerTimer += Time.deltaTime; // Increment the timer if the player is not seen

            if (lostPlayerTimer >= patrolTime)
            {
                lostPlayerTimer = 0f; // Reset the timer
                TransitionToState(patrolState); // Transition to patrol state after a certain time
            }

        }

    }

    private void TransitionToState(EnemyBaseState newState)
    {
        if (newState == currentState) return; // No transition if the state is the same

        currentState.OnExitState(); // Exit the current state
        currentState = newState; // Set the new state
        currentState.OnEnterState2(doorPosition); // Enter the new state
        currentState.OnEnterState(); // Enter the new state
    }

    private void OnDoorEvent(OpenCloseDoor door)
    {
        doorTrigger = true; // Set the door trigger flag to true
        doorPosition = door.transform.position; // Store the position of the door when triggered
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
