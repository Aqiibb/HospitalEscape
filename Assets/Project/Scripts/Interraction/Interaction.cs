using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField] private string[] interactableTags; // Array of tags for interactable objects
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

        // Check if the ray hits any of the interactable tags
        if (Physics.Raycast(ray, out hit))
        {
            bool isInteractable = false;
            foreach (string tag in interactableTags)
            {
                if (hit.collider.CompareTag(tag))
                {
                    isInteractable = true;
                    break;
                }
            }

            if (isInteractable)
            {
                // Set image active and display interaction message (optional)
                interactionImage.gameObject.SetActive(true);
                // You can change the image sprite or other properties here as needed
            }
            else
            {
                // Set image inactive if not hitting an interactable object
                interactionImage.gameObject.SetActive(false);
            }
        }
        else
        {
            // Set image inactive if not hitting anything
            interactionImage.gameObject.SetActive(false);
        }
    }

    void TryInteract()
    {
        // Raycast or other interaction detection logic (modify as needed)
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 20f))
        {
            // Implement your interaction logic here
        }
    }
}