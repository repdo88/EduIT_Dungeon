using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ChestOpen : MonoBehaviour, Interactable
{
    private Animator chestAnimator;
    private int animIDOpen;
    public bool isOpen = false; // Track if the chest is open
    private bool hasAnimator;
    [SerializeField] private GameObject chestLight; // Reference to the light GameObject
    public UnityEvent chestOpen; // Event to trigger when the chest is opened
    private AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        hasAnimator = TryGetComponent<Animator>(out chestAnimator);
        animIDOpen = Animator.StringToHash("Open");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {
        
        if (hasAnimator)
        {
            chestAnimator.SetTrigger(animIDOpen);
            isOpen = true; // Set the chest to open state
            chestOpen?.Invoke(); // Trigger the chest open event
            chestLight.SetActive(false);
            audioSource?.Play(); // Play the audio if the AudioSource is present
        }
    }

}
