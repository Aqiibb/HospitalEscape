using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Add this line

public class CursorLock : MonoBehaviour
{
    private bool isCursorLocked = true;

    void Start()
    {
        LockCursor(); // Call the function here
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsMouseOverUI()) // Check Escape only if not over UI
        {
            ToggleCursorLock();
        }
    }

    void ToggleCursorLock() // Implement toggle logic here
    {
        // ... (code to toggle cursor lock state)
    }

    void LockCursor() // Implement cursor locking here
    {
        // ... (code to lock cursor)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    bool IsMouseOverUI()
    {
        if (EventSystem.current != null)
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
        else
        {
            // Handle the case where EventSystem is null (return false or some default behavior)
            return false; // Example: Assume no UI interaction if EventSystem is missing
        }
    }
}