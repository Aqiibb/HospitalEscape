using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // Check for input to reset the object.
        if (Input.GetButtonDown("ResetObject"))
        {
            // Reload the current scene using the SceneManager (for Unity 5.3 and later).
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}