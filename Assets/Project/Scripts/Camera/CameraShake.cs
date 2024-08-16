using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraShake : MonoBehaviour
{
    // Intensity of the shake
    public float shakeIntensity = 0.1f;
    // Duration of the shake
    public float shakeDuration = 0.5f;

    // Initial position of the camera
    private Vector3 initialPosition;

    private void Start()
    {
        // Store the initial position of the camera
        initialPosition = transform.localPosition;
    }

    public void Shake()
    {
        // Start the coroutine to shake the camera
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        // Shake the camera for the specified duration
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            // Calculate a random offset for the camera position
            Vector3 randomOffset = Random.insideUnitSphere * shakeIntensity;
            randomOffset.z = 0; // Ensure the shake is only in 2D

            // Apply the offset to the camera position
            transform.localPosition = initialPosition + randomOffset;

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Reset the camera position after shaking
        transform.localPosition = initialPosition;
    }
}