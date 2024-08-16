using UnityEngine;

namespace YourProject.NewInteractionSystem
{
    public class NewPlayerInteractionHandler : MonoBehaviour
    {
        [Header("Settings")]
        public float interactionRange = 2f; // Range for interactable objects
        public float collectableRange = 5f; // Range specifically for collectables

        [Header("Layer Settings")]
        public LayerMask interactableLayer; // Set in inspector for interactable objects and collectables

        public Camera playerCamera;
        public KeyCode interactKey = KeyCode.E;

        private INewInteractable currentInteractable;
        private Vector3 interactionPromptPosition;

        private void Update()
        {
            CheckForInteractables();
            HandleInteractionInput();
        }

        private void CheckForInteractables()
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, interactableLayer))
            {
                var interactable = hit.collider.GetComponent<INewInteractable>();
                if (interactable != null)
                {
                    HandleInteractionUI(interactable, hit.collider.transform);
                }
                else
                {
                    HideInteractionPrompt();
                }
            }
            else
            {
                HideInteractionPrompt();
            }
        }

        private void HandleInteractionUI(INewInteractable interactable, Transform interactableTransform)
        {
            float distance = Vector3.Distance(transform.position, interactableTransform.position);
            bool isCollectable = interactable is NewCollectableItem;

            if ((isCollectable && distance <= collectableRange) || (!isCollectable && distance <= interactionRange))
            {
                // Show interaction prompt directly above the object
                interactionPromptPosition = interactableTransform.position + Vector3.up * 2f; // Adjust height above the object
                Debug.Log($"Interactable: {interactable.GetInteractionPrompt()} at position: {interactionPromptPosition}");
                currentInteractable = interactable;
            }
            else
            {
                HideInteractionPrompt();
            }
        }

        private void HideInteractionPrompt()
        {
            // Here we just clear the currentInteractable without any UI changes
            currentInteractable = null;
        }

        private void HandleInteractionInput()
        {
            if (currentInteractable != null && Input.GetKeyDown(interactKey))
            {
                if (currentInteractable is NewCollectableItem collectable)
                {
                    float distance = Vector3.Distance(transform.position, collectable.transform.position);
                    if (distance <= collectableRange)
                    {
                        collectable.Collect();
                    }
                }
                else
                {
                    currentInteractable.Interact();
                }
            }
        }
    }
}