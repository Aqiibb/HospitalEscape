using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interract : MonoBehaviour
{
    // Add necessary variables, such as flags for interaction, animations, etc.

    public float rotationSpeed = 5f; // Adjust the rotation speed as needed
    public float scaleSpeed = 0.1f; // Adjust the scaling speed as needed

    void Update()
    {
        // Check for mouse wheel input
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheelInput != 0f)
        {
            // Perform rotation or scaling based on mouse wheel input
            PerformInteraction(scrollWheelInput);
        }
    }

    void PerformInteraction(float scrollDelta)
    {
        // Check if the object should be rotated or scaled based on your design
        // For example, you can rotate the object based on the mouse wheel input
        // or scale it up/down.

        // Rotate the object based on the mouse wheel input
        transform.Rotate(Vector3.up, rotationSpeed * scrollDelta * Time.deltaTime);

        // Or, scale the object based on the mouse wheel input
        // float newScale = Mathf.Clamp(transform.localScale.x + scaleSpeed * scrollDelta, minScale, maxScale);
        // transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}