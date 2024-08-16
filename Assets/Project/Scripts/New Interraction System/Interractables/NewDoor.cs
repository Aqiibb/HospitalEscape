using UnityEngine;

namespace YourProject.NewInteractionSystem
{
    public class NewDoor : MonoBehaviour, INewInteractable
    {
        public string interactionPrompt = "Press E to Open Door";

        public void Interact()
        {
            Debug.Log("Door opened!");
            // Add logic to open the door here, e.g., animations, sounds
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public string GetInteractionPrompt()
        {
            return interactionPrompt;
        }
    }
}