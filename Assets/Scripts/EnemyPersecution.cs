using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPersecution : MonoBehaviour
{
    [SerializeField] GameObject Player; // Reference to the player GameObject
    private NavMeshAgent agent; // Reference to the NavMeshAgent component

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(Player.transform.position);
    }
}
