using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateAttack : EnemyBaseState
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootInterval = 5f;
    [SerializeField] private Transform playerTransform;
    private float shootTimer;
    [SerializeField] private float attackSpeed = 0.1f; // Speed of the chase

    private Animator animator; // Animator component for the enemy
    private bool hasAnimator; // Flag to check if the enemy has an animator component
    private int animIDAtack; // Animator ID for attack animation
    private NavMeshAgent agent; // Reference to the NavMeshAgent component

    [Header("Sound settings")]
    [SerializeField] private AudioSource lichAudioSource; 
    [SerializeField] private AudioClip fireBallSound; 
    public override void OnEnterState()
    {
        base.OnEnterState();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = attackSpeed; // Set the speed of the agent
        shootTimer = shootInterval;
        hasAnimator = TryGetComponent<Animator>(out animator); // Check if the enemy has an animator component
        animIDAtack = Animator.StringToHash("Attack"); // Initialize the animator ID for attack animation
        StartCoroutine(Shoot()); // Start the shooting coroutine
    }

    public override void UpdateState()
    {
        base.UpdateState();
        agent.SetDestination(playerTransform.position);
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            StartCoroutine(Shoot()); // Start the shooting coroutine
            shootTimer = shootInterval;
        }
    }

    private IEnumerator Shoot()
    {
        //if (playerTransform == null) return;
        Debug.Log("Shooting Fireball");
        animator?.SetTrigger(animIDAtack); // Trigger the attack animation if animator exists
        yield return new WaitForSeconds(0.5f); // Esperamos un tiempo antes de disparar
        // Instanciamos la bola de fuego
        var fireballObj = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        lichAudioSource?.PlayOneShot(fireBallSound); // Play the fireball sound if audio source exists

        // Inicializamos su dirección hacia el jugador
        var fireball = fireballObj.GetComponent<FireBall>();
        if (fireball != null)
            fireball.Initialized(playerTransform);
        else
            Debug.LogError("El prefab no tiene el componente Fireball.");


    }








}