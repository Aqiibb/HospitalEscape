using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour, IInteractable
{
    [Header("Switch Settings")]
    public bool isSwitchOn = false;

    [Header("Events")]
    public UnityEvent onSwitchOn;
    public UnityEvent onSwitchOff;

    // Implement the Interact method from the IInteractable interface
    public void Interact()
    {
        // Toggle the switch state when interacting
        ToggleSwitchState();
    }

    void ToggleSwitchState()
    {
        // Toggle the switch state
        isSwitchOn = !isSwitchOn;

        // Trigger events based on the switch state
        if (isSwitchOn)
        {
            onSwitchOn.Invoke();
        }
        else
        {
            onSwitchOff.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // You can add additional logic here if needed
    }
}
