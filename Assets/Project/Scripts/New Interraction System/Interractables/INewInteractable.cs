using UnityEngine;

namespace YourProject.NewInteractionSystem
{
    public interface INewInteractable
    {
        void Interact();
        Vector3 GetPosition();
        string GetInteractionPrompt();
    }
}