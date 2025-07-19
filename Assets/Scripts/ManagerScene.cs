using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScene : MonoBehaviour
{
    public static ManagerScene Instance; // Singleton instance of the ManagerScene class
    [SerializeField] GameObject pressEText; // Reference to the UI text object that prompts the player to press 'E'
    [SerializeField] GameObject attackText; // Reference to the UI text object that prompts the player to attack
    [SerializeField] GameObject gameOverBackground; // Reference to the player GameObject


    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Assign the singleton instance
            
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void ShowPressText()
    {
        if (pressEText != null)
        {
            pressEText.SetActive(true); // Show the 'Press E' text
        }
    }

    public void HidePressText()
    {
        if (pressEText != null)
        {
            pressEText.SetActive(false); // Hide the 'Press E' text
        }
    }

    public void ShowAttackText()
    {
        if (attackText != null)
        {
            attackText.SetActive(true); 
        }
    }

    public void HideAttackText()
    {
        if (attackText != null)
        {
            attackText.SetActive(false); 
        }
    }
    public void ShowGameOverBackground()
    {
        if (gameOverBackground != null)
        {
            gameOverBackground.SetActive(true); // Show the game over background
        }
    }
}
