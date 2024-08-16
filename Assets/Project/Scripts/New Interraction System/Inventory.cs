using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YourProject.NewInteractionSystem
{
    public class Inventory : MonoBehaviour
    {
        [Header("Inventory Settings")]
        public List<InventoryItem> items = new List<InventoryItem>(); // List of inventory items

        [Header("UI Elements")]
        public Text inventoryUIText; // Reference to a UI Text element to display inventory

        private void Start()
        {
            if (inventoryUIText == null)
            {
                Debug.LogError("Inventory UI Text is not assigned.");
            }
            UpdateInventoryUI(); // Initialize UI on start
        }

        public void AddItem(string itemName, int quantity = 1)
        {
            InventoryItem existingItem = items.Find(i => i.itemName == itemName);
            if (existingItem != null)
            {
                existingItem.quantity += quantity;
            }
            else
            {
                items.Add(new InventoryItem { itemName = itemName, quantity = quantity });
            }
            Debug.Log("Added item: " + itemName + " Quantity: " + quantity);
            UpdateInventoryUI(); // Update the UI whenever an item is added
        }

        public void RemoveItem(string itemName, int quantity = 1)
        {
            InventoryItem existingItem = items.Find(i => i.itemName == itemName);
            if (existingItem != null)
            {
                existingItem.quantity -= quantity;
                if (existingItem.quantity <= 0)
                {
                    items.Remove(existingItem);
                }
                Debug.Log("Removed item: " + itemName + " Quantity: " + quantity);
                UpdateInventoryUI(); // Update the UI whenever an item is removed
            }
            else
            {
                Debug.LogWarning("Attempted to remove item that does not exist: " + itemName);
            }
        }

        private void UpdateInventoryUI()
        {
            if (inventoryUIText != null)
            {
                inventoryUIText.text = "Inventory:\n";
                foreach (InventoryItem item in items)
                {
                    inventoryUIText.text += $"{item.itemName} (x{item.quantity})\n";
                }
                if (items.Count == 0)
                {
                    inventoryUIText.text += "Empty";
                }
            }
        }
    }

    [System.Serializable]
    public class InventoryItem
    {
        public string itemName;
        public int quantity;
    }
}