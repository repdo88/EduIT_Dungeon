using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterLife : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    public UnityEvent onDeath;
    private GameObject player; // Reference to the player GameObject
    [SerializeField] private GameObject dagger; // Reference to the dagger GameObject

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject; // Initialize the player GameObject reference
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if ((layerMask.value & (1 << collider.transform.gameObject.layer)) > 0)
        {
            Debug.Log("Character hit by enemy!");
            onDeath.Invoke(); // Trigger the death event
            player.SetActive(false); // Deactivate the player GameObject
        }
    }

    public void getDagger()
    {
        dagger.SetActive(true); // Activate the dagger GameObject
    }
}
