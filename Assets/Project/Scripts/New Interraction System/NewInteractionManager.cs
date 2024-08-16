using System.Collections.Generic;
using UnityEngine;

namespace YourProject.NewInteractionSystem
{
    public class NewInteractionManager : MonoBehaviour
    {
        public static NewInteractionManager Instance { get; private set; }

        private List<INewInteractable> interactables = new List<INewInteractable>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void RegisterInteractable(INewInteractable interactable)
        {
            if (!interactables.Contains(interactable))
            {
                interactables.Add(interactable);
            }
        }

        public void DeregisterInteractable(INewInteractable interactable)
        {
            if (interactables.Contains(interactable))
            {
                interactables.Remove(interactable);
            }
        }

        public INewInteractable GetClosestInteractable(Vector3 position, float range)
        {
            INewInteractable closest = null;
            float closestDistance = range;

            foreach (var interactable in interactables)
            {
                float distance = Vector3.Distance(position, interactable.GetPosition());
                if (distance < closestDistance)
                {
                    closest = interactable;
                    closestDistance = distance;
                }
            }

            return closest;
        }
    }
}