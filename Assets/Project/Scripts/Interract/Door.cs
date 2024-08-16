using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // Add door-specific interaction logic here
        Debug.Log("Door opened!");
    }
}