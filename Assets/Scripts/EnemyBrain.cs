using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float distanceToPlayer;

    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent agent;

    private ThirdPersonController playerController;
    [SerializeField] private bool playerIsCrouch;
    [SerializeField] private float playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        playerController = ThirdPersonController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSensors();
    }

    private void UpdateSensors()
    {
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
        //calculate angle to player
        float angleToPlayer = Vector3.Dot(transform.forward, directionToPlayer);
        //bools to see the state of the sensors
        inAngleVision = angleToPlayer > (1 - tempVisionAngleTheshold);
        inRangeVision = distanceToPlayer < visionMaxDistance;
        inMinRadius = distanceToPlayer < minRadius;
        

        if ((inAngleVision && inRangeVision) || inMinRadius)
        {
            seePlayer = true;
            //Debug.Log("Player is in vision cone and range");
        }
        else
        {
            seePlayer = false;
            //Debug.Log("Player is not in vision cone or range");
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
        Gizmos.DrawRay(transform.position, directionToPlayer.normalized * visionMaxDistance);
    }

}
