using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    [SerializeField] float visionRange = 5f; // Range of the player's vision
    [SerializeField] LayerMask interactableLayer; // Layer mask to filter interactable objects
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray vision = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(vision, out RaycastHit hitInfo, visionRange, interactableLayer))
            {
                
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    Debug.Log("Hit");
                    interactable.Interact(); // Call the Interact method on the interactable object
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * visionRange);
    }
}
