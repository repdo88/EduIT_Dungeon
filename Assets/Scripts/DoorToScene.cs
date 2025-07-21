using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DoorToScene : MonoBehaviour, Interactable
{
    public UnityEvent onInteract; // Event to notify when the door is interacted with
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        onInteract?.Invoke(); // Notify subscribers that the door is being interacted with
    }

}
