using UnityEngine;

namespace YourProject.NewInteractionSystem
{
    public class NewCollectableItem : NewInteractableObject, ICollectable
    {
        [Header("Item Settings")]
        public string itemName = "Collectable Item"; // Name of the item
        public bool isCollectable = true; // Indicates if the item can be collected
        public bool isInteractable = false; // Indicates if the item can be interacted with

        private void OnTriggerEnter(Collider other)
        {
            if (isCollectable && other.CompareTag("Player")) // Ensure the collider belongs to the player
            {
                Collect();
            }
        }

        public string GetItemName()
        {
            return itemName;
        }

        public override void Interact()
        {
            if (isInteractable)
            {
                Debug.Log($"{itemName} has been interacted with.");
            }
        }

        public new void Collect()
        {
            base.Collect();
            Debug.Log($"{itemName} has been collected.");
            Destroy(gameObject); // Optionally destroy the object after collecting
        }
    }
}