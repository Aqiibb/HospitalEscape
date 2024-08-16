using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // Add NPC-specific interaction logic here
        Debug.Log("Hello, player!");
    }
}