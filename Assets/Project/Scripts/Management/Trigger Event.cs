using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private float timeToTrigger = 5f; // Time in seconds until the event triggers
    public UnityEvent onEventTrigger; // Event to be triggered after the specified time

    private float timer = 0f; // Internal timer to track elapsed time

    private void Update()
    {
        timer += Time.deltaTime; // Update timer each frame

        if (timer >= timeToTrigger)
        {
            // Trigger the event
            onEventTrigger?.Invoke();

            // Optional: Reset timer or disable script after trigger
            // timer = 0f;  // Reset timer (uncomment if needed)
            // enabled = false; // Disable script after trigger (uncomment if needed)
        }
    }
}