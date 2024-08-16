using UnityEngine;
using UnityEngine.UI;

public class InteractionIndicator : MonoBehaviour
{
    [SerializeField] private string targetTag = "InteractiveObject"; // Change this to the tag you want to interact with
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    public Image interactionImage;

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }

        // Cast a ray from the center of the screen
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Check if the ray hits an object with the specified tag
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag(targetTag))
        {
            // Set image active and display interaction message
            interactionImage.gameObject.SetActive(true);
            // You can change the image sprite or other properties here as needed
        }
        else
        {
            // Set image inactive if not hitting the target
            interactionImage.gameObject.SetActive(false);
        }
    }

    void TryInteract()
    {
        // Raycast or other interaction detection logic
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 20f))
        {
            Debug.Log("TryInteract with object ID: " + hit.collider.gameObject.GetInstanceID());
        }
    }
}