using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemCollectedEvent : UnityEvent<string> { } // Custom event class for item collection

public class InventoryManager : MonoBehaviour
{
    public bool hasCrowbar;
    public bool hasFlashlight;

    public GameObject crowbarPrefab; // Prefab of the crowbar item (visual representation, likely a UI element on the canvas)
    public GameObject flashlightPrefab; // Prefab of the flashlight item (visual representation, likely a UI element on the canvas)

    private Camera mainCamera;

    // Removed references to canvas and interactionIndicator

    public ItemCollectedEvent onCrowbarCollected; // Custom event for crowbar collection
    public ItemCollectedEvent onFlashlightCollected; // Custom event for flashlight collection

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Check for item collection using raycast
        if (Input.GetKeyDown(KeyCode.E)) // Replace KeyCode.E with your desired interaction key
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 2f)) // Adjust max distance as needed
            {
                if (hit.collider.gameObject.CompareTag("Crowbar"))
                {
                    CollectItem(crowbarPrefab, ref hasCrowbar, "Crowbar");
                    Destroy(hit.collider.gameObject); // Destroy collected crowbar
                    onCrowbarCollected?.Invoke("Crowbar"); // Trigger crowbar collection event
                }
                else if (hit.collider.gameObject.CompareTag("Flashlight"))
                {
                    CollectItem(flashlightPrefab, ref hasFlashlight, "Flashlight");
                    Destroy(hit.collider.gameObject); // Destroy collected flashlight
                    onFlashlightCollected?.Invoke("Flashlight"); // Trigger flashlight collection event
                }
            }
        }
    }

    private void CollectItem(GameObject itemPrefab, ref bool hasItem, string itemName)
    {
        if (!hasItem)
        {
            // Spawn the item visually (optional, if needed for inventory UI)
            // Instantiate(itemPrefab, transform.position, transform.rotation);
            hasItem = true;
            Debug.Log("Collected " + itemName);
        }
    }

    // Implement functions to use crowbar and flashlight based on their collected state (hasCrowbar and hasFlashlight)
    public void UseCrowbar()
    {
        if (hasCrowbar)
        {
            // Implement crowbar functionality
            Debug.Log("Using Crowbar!");
        }
        else
        {
            Debug.Log("You don't have the crowbar!");
        }
    }

    public void UseFlashlight()
    {
        if (hasFlashlight)
        {
            // Implement flashlight functionality here (e.g., toggle light on/off)
            Debug.Log("Toggling Flashlight!");
        }
        else
        {
            Debug.Log("You don't have the flashlight!");
        }
    }
}