using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Utility
{
    public class SimpleActivatorMenu : MonoBehaviour
    {
        public Text camSwitchButton; // Use the Text component for displaying the active object's name.
        public GameObject[] objects;

        private int currentActiveObject;

        private void OnEnable()
        {
            // The active object starts from the first in the array.
            currentActiveObject = 0;
            camSwitchButton.text = objects[currentActiveObject].name;
        }

        public void NextCamera()
        {
            int nextActiveObject = currentActiveObject + 1 >= objects.Length ? 0 : currentActiveObject + 1;

            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(i == nextActiveObject);
            }

            currentActiveObject = nextActiveObject;
            camSwitchButton.text = objects[currentActiveObject].name;
        }
    }
}