using System.Collections;
using UnityEngine;

public class EventDelayed : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public string cutsceneAnimationName = "Cutscene"; // Name of the cutscene animation
    public float delayBeforeEvent = 5.0f; // Delay before triggering the event

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to play the cutscene and trigger events
        StartCoroutine(PlayCutsceneAndTriggerEvent());
    }

    // Coroutine to handle the cutscene and delayed events
    IEnumerator PlayCutsceneAndTriggerEvent()
    {
        // Play the cutscene animation
        animator.Play(cutsceneAnimationName);

        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeEvent);

        // Trigger the event after the delay
        TriggerEvent();
    }

    // Method to handle the event
    void TriggerEvent()
    {
        // Add your event logic here
        Debug.Log("Event triggered after delay!");
        // For example, you could enable an object, play a sound, etc.
    }
}