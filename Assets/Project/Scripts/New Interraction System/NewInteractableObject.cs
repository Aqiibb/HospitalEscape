using UnityEngine;

namespace YourProject.NewInteractionSystem
{
    public class NewInteractableObject : MonoBehaviour, INewInteractable
    {
        public enum InteractableType
        {
            Collectable,
            Door
        }

        [Header("Interactable Settings")]
        public InteractableType interactableType = InteractableType.Collectable;
        public string interactionPrompt = "Press E to Interact";

        private Inventory inventory;

        private void Start()
        {
            inventory = FindObjectOfType<Inventory>();
            if (inventory == null)
            {
                Debug.LogError("No Inventory found in the scene.");
            }
        }

        public virtual void Interact()
        {
            switch (interactableType)
            {
                case InteractableType.Collectable:
                    Collect();
                    break;
                case InteractableType.Door:
                    OpenDoor();
                    break;
            }
        }

        public virtual void Collect()
        {
            if (interactableType == InteractableType.Collectable)
            {
                if (inventory != null)
                {
                    inventory.AddItem(GetItemName());
                }
                else
                {
                    Debug.LogWarning("No Inventory found.");
                }

                Debug.Log($"{GetItemName()} collected!");
                Destroy(gameObject); // Optionally destroy the object after collecting
            }
        }

        public virtual void OpenDoor()
        {
            if (interactableType == InteractableType.Door)
            {
                Debug.Log("Door opened!");
                // Add logic to open the door here, e.g., animations, sounds
            }
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public string GetInteractionPrompt()
        {
            return interactionPrompt;
        }

        public virtual string GetItemName()
        {
            return "Base Item";
        }
    }
}