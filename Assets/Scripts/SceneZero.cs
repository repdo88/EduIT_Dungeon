using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneZero : MonoBehaviour
{
    public static SceneZero Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }


    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1); // Load the first dungeon scene
    }
}
