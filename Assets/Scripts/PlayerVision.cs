using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerVision : MonoBehaviour
{
    [SerializeField] GameObject eyes; // Reference to the player's eyes GameObject
    
    [SerializeField] float visionRange = 5f; // Range of the player's vision
    [SerializeField] LayerMask interactableLayer; // Layer mask to filter interactable objects
    [SerializeField] LayerMask chestLayer; // Layer mask to filter interactable objects
    public UnityEvent onInteractRange; // Event to trigger when an interaction is possible
    public UnityEvent onInteractNotRange; // Event to trigger when no interactable objects are in range
    public bool canAttack = false; // Flag to check if the player can attack
    [SerializeField] float visionAttack = 3f; // Range of the player attack vision
    [SerializeField] LayerMask attackableLayer; // Layer mask to filter interactable objects
    public UnityEvent onAttackRange; // Event to trigger when an attack is possible
    public UnityEvent onAttackNotRange; // Event to trigger when no attackable objects are in range

    private StarterAssetsInputs _input; // Reference to the StarterAssetsInputs component for input handling
    


    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        // Head ray for door interaction
        Ray vision = new Ray(eyes.transform.position, eyes.transform.forward);
        if (Physics.Raycast(vision, out RaycastHit hitInfo, visionRange, interactableLayer))
        {
            onInteractRange.Invoke(); // Trigger the interaction event if an interactable object is in range
            //if (Input.GetKeyDown(KeyCode.E))
            if (_input.interact)
            {

                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                if (interactable != null)
                {

                    interactable.Interact(); // Call the Interact method on the interactable object
                }
            }
        }
        else if (Physics.Raycast(vision, out RaycastHit hitInfo3, visionRange, chestLayer))
        {
            if (!hitInfo3.collider.GetComponent<ChestOpen>().isOpen)
            {
                onInteractRange.Invoke(); // Trigger the interaction event if an interactable object is in range
                //if (Input.GetKeyDown(KeyCode.E))
                if (_input.interact)
                {


                    Interactable interactable2 = hitInfo3.collider.GetComponent<Interactable>();
                    if (interactable2 != null)
                    {

                        interactable2.Interact(); // Call the Interact method on the interactable object
                    }
                }
            }
            else
            {
                onInteractNotRange.Invoke(); // Trigger the no interaction event if the chest is already open
            }
        }
        else
        {
            onInteractNotRange.Invoke(); // Trigger the no interaction event if no interactable objects are in range
        }
        _input.interact = false; // Reset the interact input to prevent repeated interactions


        // Check if the player can attack
        if (Physics.Raycast(vision, out RaycastHit hitInfo2, visionAttack, attackableLayer))
        {
            var enemyBrain = hitInfo2.collider.GetComponent<Enemy2Brain>(); 
            if (enemyBrain != null)
            {
                if (!enemyBrain.seePlayer)
                {
                    onAttackRange.Invoke(); // Trigger the attack event if an attackable object is in range
                    canAttack = true; // Set the canAttack flag to true
                }
                else
                {
                    onAttackNotRange.Invoke(); // Trigger the no attack event if the enemy sees the player
                    canAttack = false; // Set the canAttack flag to false
                }

            }
            else
            {
                onAttackNotRange.Invoke(); // Trigger the no attack event if no attackable objects are in range
                canAttack = false; // Set the canAttack flag to false
            }
        }
        else
        {
            onAttackNotRange.Invoke(); // Trigger the no attack event if no attackable objects are in range
            canAttack = false; // Set the canAttack flag to false
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(eyes.transform.position, eyes.transform.forward * visionRange);
        
    }
}
