using UnityEngine;

public class GunBobbing : MonoBehaviour
{
    [SerializeField] float bobbingSpeed = 1f; // Speed of the bobbing movement
    [SerializeField] float bobbingAmount = 0.1f; // Amount of bobbing movement
    [SerializeField] Transform cameraTransform; // Reference to the camera's transform

    private Vector3 originalPosition; // Original local position of the gun

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // Calculate the vertical movement based on a sine wave function
        float verticalMovement = Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;

        // Calculate the horizontal movement based on camera rotation
        float horizontalMovement = -Mathf.Sin(cameraTransform.eulerAngles.y * Mathf.Deg2Rad) * bobbingAmount;

        // Update the position of the gun along the vertical and horizontal axes
        transform.localPosition = originalPosition + new Vector3(horizontalMovement, verticalMovement, 0f);
    }
}