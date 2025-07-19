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

    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    public override void OnEnterState()
    {
        base.OnEnterState();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = attackSpeed; // Set the speed of the agent
        shootTimer = shootInterval;

    }

    public override void UpdateState()
    {
        base.UpdateState();
        agent.SetDestination(playerTransform.position);
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }

    void Shoot()
    {
        if (playerTransform == null) return;
        Debug.Log("Shooting Fireball");

        // Instanciamos la bola de fuego
        var fireballObj = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

        // Inicializamos su dirección hacia el jugador
        var fireball = fireballObj.GetComponent<FireBall>();
        if (fireball != null)
            fireball.Initialized(playerTransform);
        else
            Debug.LogError("El prefab no tiene el componente Fireball.");


    }








}