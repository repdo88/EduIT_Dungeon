using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScene : MonoBehaviour
{
    public static ManagerScene Instance; // Singleton instance of the ManagerScene class
    [SerializeField] GameObject pressEText; // Reference to the UI text object that prompts the player to press 'E'
    [SerializeField] GameObject attackText; // Reference to the UI text object that prompts the player to attack
    [SerializeField] GameObject gameOverBackground; // Reference to the player GameObject
    [SerializeField] GameObject winBackgounrd;
    [SerializeField] GameObject chestText; // Reference to the player GameObject
    [SerializeField] GameObject startImage;
    private bool isDead = false; // Flag to check if the player is dead
    [SerializeField] private GameObject[] openableDoors; // Doors that only can be open after an event

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

    public void Update()
    {
        if (isDead && Input.GetKeyDown(KeyCode.Return))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0); // Load the Main Menu scene
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (startImage != null)
            {
                startImage.SetActive(false); // Hide the start image when 'Return' is pressed
            }
            if (chestText != null)
            {
                chestText.SetActive(false); // Hide the chest text when 'Return' is pressed
            }
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
            isDead = true; // Set the isDead flag to true
        }
    }

    public void GoToSecondDungeon()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2); // Load the second dungeon scene
    }

    public void makeDoorsOpenable()
    {
        if (chestText != null)
        {
            chestText.SetActive(true); // Show the chest text
        }
            
        int targetLayer = LayerMask.NameToLayer("Door"); // Get the layer index for "OpenableDoors"
        foreach (GameObject door in openableDoors)
        {
            if (door != null)
            {
                door.layer = targetLayer; // Set the layer of each door to "OpenableDoors"
            }
        }
    }

    public void showWinBackground()
    {
        if (winBackgounrd != null)
        {
            winBackgounrd.SetActive(true); // Show the win background
            
        }
    }
}
