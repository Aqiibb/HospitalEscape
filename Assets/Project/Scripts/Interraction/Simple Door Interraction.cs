using UnityEngine;
using UnityEngine.Events;

public class SimpleDoorInteraction : MonoBehaviour
{
    public bool isOpen; // Flag to track door state (open or closed)
    public float triggerRadius; // Radius of the trigger collider for player interaction (optional)

    public KeyCode interactionKey = KeyCode.E; // Key for interacting with the door

    public UnityEvent onDoorOpen; // Event triggered when the door opens
    public UnityEvent onDoorClose; // Event triggered when the door closes

    private Collider doorTrigger; // Reference to the trigger collider attached to the door (optional)

    private void Start()
    {
        // Optional: Set initial door state based on Inspector settings
        if (isOpen)
        {
            OpenDoor();
        }

        // Get the trigger collider attached to the door (if any)
        doorTrigger = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactionKey)) // Check for E key press
        {
            if (doorTrigger != null) // Check if trigger collider exists
            {
                // Cast a ray from the camera to check for interaction within trigger radius
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, triggerRadius))
                {
                    // Check if the hit object has the "Door" tag and is the trigger collider
                    if (hit.collider.gameObject.CompareTag("Door") && hit.collider == doorTrigger)
                    {
                        if (!isOpen)
                        {
                            OpenDoor();
                        }
                        else
                        {
                            CloseDoor();
                        }
                    }
                }
            }
            else // If no trigger collider, check for direct collision with a "Door" tagged object
            {
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 2f)) // Adjust max distance as needed
                {
                    if (hit.collider.gameObject.CompareTag("Door") && hit.collider.gameObject == gameObject) // Check for direct collision with the door itself
                    {
                        if (!isOpen)
                        {
                            OpenDoor();
                        }
                        else
                        {
                            CloseDoor();
                        }
                    }
                }
            }
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        onDoorOpen?.Invoke(); // Trigger event
    }

    private void CloseDoor()
    {
        isOpen = false;
        onDoorClose?.Invoke(); // Trigger event
    }
}