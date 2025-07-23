using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource; // Reference to the AudioSource component
    [SerializeField] private AudioClip playerDeath; // Sound to play when the player dies
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this; // Assign the singleton instance
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component attached to this GameObject
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPlayerDeathSound()
    {
        if (audioSource != null && playerDeath != null)
        {
            audioSource.PlayOneShot(playerDeath); // Play the player death sound
        }
    }

}
