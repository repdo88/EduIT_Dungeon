using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenCloseDoor : MonoBehaviour, Interactable
{
    private Animator doorAnimator;
    private int animIDOpen = Animator.StringToHash("Open");
    private int animIDInteract = Animator.StringToHash("Interact");
    private bool hasAnimator;
    private bool isOpen = false;
    public static event Action<OpenCloseDoor> OnDoorInteract; // Event to notify when the door is interacted with

    // Start is called before the first frame update
    void Start()
    {
        hasAnimator = TryGetComponent<Animator>(out doorAnimator);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (hasAnimator)
        {
            doorAnimator.SetTrigger(animIDInteract);
            
            if (!isOpen)
            {
                OnDoorInteract?.Invoke(this); // Notify subscribers that the door is being interacted with
                doorAnimator.SetBool(animIDOpen, true);
                isOpen = !isOpen; // Toggle the open state
            }
            else
            {
                OnDoorInteract?.Invoke(this); // Notify subscribers that the door is being interacted with
                doorAnimator.SetBool(animIDOpen, false);
                isOpen = !isOpen; // Toggle the open state
            }
            
        }
    }

}
