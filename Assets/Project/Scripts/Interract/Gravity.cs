using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] private Vector3 defaultGravityDirection = Vector3.down; // Default gravity direction is down
    [SerializeField] private float gravityStrength = 9.81f;

    private Vector3 originalGravity; // Store original gravity direction

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            originalGravity = Physics.gravity; // Store the original gravity
            SetGravityDirection(-transform.up); // Flips gravity for the player
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            SetGravityDirection(defaultGravityDirection); // Resets gravity for the player
    }

    private void SetGravityDirection(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.down, direction);
        transform.rotation = targetRotation * Quaternion.Euler(180, 0, 0); // Adjust for correct orientation
        Physics.gravity = direction * gravityStrength;
    }
}