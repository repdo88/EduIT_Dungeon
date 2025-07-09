using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour, Interactable
{
    private Animator doorAnimator;
    private int animIDOpen = Animator.StringToHash("Open");
    private int animIDInteract = Animator.StringToHash("Interact");
    private bool hasAnimator;
    private bool isOpen = false;

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
                doorAnimator.SetBool(animIDOpen, true);
            }
            else
            {
                doorAnimator.SetBool(animIDOpen, false);
            }
            isOpen = !isOpen; // Toggle the open state
        }
    }

}
