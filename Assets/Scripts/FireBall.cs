using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] float speed = 10f; // Speed of the fireball

    private Vector3 direction; // Direction of the fireball

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime; // Move the fireball towards the player   
    }

    public void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject); // Destroy the fireball
    }

    public void Initialized(Transform playerPos)
    {
        direction = (playerPos.position - transform.position).normalized; // Recalculate the direction if needed
    }
}
