using UnityEngine;
using System.Collections;

public class JumpScare : MonoBehaviour
{
    public Camera Camera;
    public GameObject scareObject; // The object to activate for the jump scare
    public float delayBeforeActivation = 1f; // The delay before activating the jump scare
    public float scareDuration = 2f; // The duration of the jump scare
    public AudioClip[] scareSounds; // Array of jump scare sound effects
    public float cameraShakeIntensity = 0.5f; // The intensity of the camera shake effect
    public float cameraShakeDuration = 0.5f; // The duration of the camera shake effect
    public bool resetGameAfterJumpScare = false; // Flag to reset the game after the jump scare

    private bool isJumpScareActive = false;
    private AudioSource audioSource;
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalCameraPosition = Camera.transform.localPosition;
        originalCameraRotation = Camera.transform.localRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isJumpScareActive)
        {
            // Trigger the jump scare after the specified delay
            Invoke("ActivateJumpScare", delayBeforeActivation);
            isJumpScareActive = true;
        }
    }

    private void ActivateJumpScare()
    {
        // Activate the scare object
        scareObject.SetActive(true);

        // Choose a random jump scare sound effect from the array
        if (scareSounds != null && scareSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, scareSounds.Length);
            audioSource.clip = scareSounds[randomIndex];
            audioSource.Play();
        }

        // Shake the camera
        StartCoroutine(CameraShake(cameraShakeIntensity, cameraShakeDuration));

        // Deactivate the scare object after the specified duration
        Invoke("DeactivateJumpScare", scareDuration);
    }

    private void DeactivateJumpScare()
    {
        // Deactivate the scare object
        scareObject.SetActive(false);
        isJumpScareActive = false;

        // Reset the camera position and rotation
        Camera.transform.localPosition = originalCameraPosition;
        Camera.transform.localRotation = originalCameraRotation;

        // Reset the game if specified
        if (resetGameAfterJumpScare)
        {
            // Add your code here to reset the game
            Debug.Log("Resetting the game...");
        }
    }

    private IEnumerator CameraShake(float intensity, float duration)
    {
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            float randomX = Random.Range(-intensity, intensity);
            float randomY = Random.Range(-intensity, intensity);

            // Calculate the shake offset
            Vector3 shakeOffset = new Vector3(randomX, randomY, 0f);

            // Apply the camera shake
            Camera.transform.localPosition = originalCameraPosition + shakeOffset;

            yield return null;
        }

        // Reset the camera position after the shake ends
        Camera.transform.localPosition = originalCameraPosition;
    }
}