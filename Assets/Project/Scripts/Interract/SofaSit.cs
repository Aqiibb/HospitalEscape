using UnityEngine;

public class SofaSit : MonoBehaviour, IInteractable
{
    public GameObject player;
    public GameObject sofaCamera;
    public float interactionDistance = 3f;

    private bool isSitting = false;

    public void ToggleSittingState()
    {
        // Toggle the sitting state
        isSitting = !isSitting;

        // Update the state of the player and sofa camera
        SetSittingState(isSitting);
    }

    void SetSittingState(bool sitting)
    {
        // Activate/deactivate the player and sofa camera based on the sitting state
        player.SetActive(!sitting);
        sofaCamera.SetActive(sitting);
    }

    // Implement the Interact method from the IInteractable interface
    public void Interact()
    {
        // Perform the interaction when the player interacts with the sofa
        ToggleSittingState();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is sitting and allow standing up with the "E" key
        if (isSitting && Input.GetKeyDown(KeyCode.E))
        {
            ToggleSittingState();
        }
    }
}