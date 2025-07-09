using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerVision : MonoBehaviour
{
    [SerializeField] float visionRange = 5f; // Range of the player's vision
    [SerializeField] LayerMask interactableLayer; // Layer mask to filter interactable objects
    public UnityEvent onInteractRange; // Event to trigger when an interaction is possible
    public UnityEvent onInteractNotRange; // Event to trigger when no interactable objects are in range



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray vision = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(vision, out RaycastHit hitInfo, visionRange, interactableLayer))
        {
            onInteractRange.Invoke(); // Trigger the interaction event if an interactable object is in range
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    Debug.Log("Hit");
                    interactable.Interact(); // Call the Interact method on the interactable object
                }
            }
        }
        else
        {
            onInteractNotRange.Invoke(); // Trigger the no interaction event if no interactable objects are in range
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * visionRange);
    }
}
