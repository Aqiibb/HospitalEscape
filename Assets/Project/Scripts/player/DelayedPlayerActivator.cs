using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedPlayerActivator : MonoBehaviour
{
    public float activationDelay = 2f; // Delay before activating the player and deactivating objects

    public GameObject playerObject; // Reference to the player object
    public GameObject[] objectsToDeactivate; // Array of game objects to deactivate

    private float timer; // Timer to track the elapsed time

    private bool isActivated = false; // Flag to check if the player and objects have been activated

    private void Update()
    {
        // Start the timer
        timer += Time.deltaTime;

        // Check if the timer has exceeded the activation delay and the player and objects have not been activated yet
        if (timer >= activationDelay && !isActivated)
        {
            ActivatePlayer(); // Activate the player
            DeactivateObjects(); // Deactivate the objects
            isActivated = true; // Set the flag to true
        }
    }

    private void ActivatePlayer()
    {
        // Activate the player object
        playerObject.SetActive(true);
    }

    private void DeactivateObjects()
    {
        // Deactivate each game object in the array
        for (int i = 0; i < objectsToDeactivate.Length; i++)
        {
            objectsToDeactivate[i].SetActive(false);
        }
    }
}
