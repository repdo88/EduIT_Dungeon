using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour, Interactable
{
    private Animator chestAnimator;
    private int animIDOpen;
    public bool isOpen = false; // Track if the chest is open
    private bool hasAnimator;
    
    


    // Start is called before the first frame update
    void Start()
    {
        hasAnimator = TryGetComponent<Animator>(out chestAnimator);
        animIDOpen = Animator.StringToHash("Open");
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
        }
    }

}
