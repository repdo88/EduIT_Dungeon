using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : EnemyBaseState
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootInterval = 5f;
    [SerializeField] private Transform playerTransform;
    private float shootTimer;
    public override void OnEnterState()
    {
        base.OnEnterState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
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


        var fireballObj = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

        var fireball = fireballObj.GetComponent<FireBall>();

        fireball.Initialized(playerTransform);


    }








}