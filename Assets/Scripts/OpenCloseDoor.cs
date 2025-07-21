using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DoorEvent : UnityEvent<OpenCloseDoor> { }
public class OpenCloseDoor : MonoBehaviour, Interactable
{
    private Animator doorAnimator;
    private int animIDOpen = Animator.StringToHash("Open");
    private int animIDInteract = Animator.StringToHash("Interact");
    private bool hasAnimator;
    private bool isOpen = false;
    public DoorEvent OnDoorInteract = new DoorEvent(); // Event to notify when the door is interacted with

    [Header("Sound settings")]
    [SerializeField] private AudioSource doorAudioSource; // Audio source for playing door sounds
    [SerializeField] private AudioClip doorOpenSound; // Sound to play when the door opens
    [SerializeField] private AudioClip doorCloseSound; // Sound to play when the door closes


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
        OnDoorInteract?.Invoke(this); // Notify subscribers that the door is being interacted with
        
        if (hasAnimator)
        {
            doorAnimator.SetTrigger(animIDInteract);
            
            if (!isOpen)
            {
                
                //OnDoorInteract?.Invoke(this); // Notify subscribers that the door is being interacted with
                doorAnimator.SetBool(animIDOpen, true);
                isOpen = !isOpen; // Toggle the open state
                if (doorAudioSource != null && doorOpenSound != null)
                {
                    doorAudioSource.PlayOneShot(doorOpenSound); // Play the door open sound
                }
            }
            else
            {
                //OnDoorInteract?.Invoke(this); // Notify subscribers that the door is being interacted with
                doorAnimator.SetBool(animIDOpen, false);
                isOpen = !isOpen; // Toggle the open state
                if (doorAudioSource != null && doorCloseSound != null)
                {
                    doorAudioSource.PlayOneShot(doorCloseSound); // Play the door close sound
                }
            }
            
        }
    }

}
