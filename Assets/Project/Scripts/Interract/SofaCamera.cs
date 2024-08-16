using UnityEngine;

public class SofaCamera : MonoBehaviour
{
    public Transform sittingPoint;
    public float rotationSpeed = 2f;
    public float maxRotationX = 80f;
    public float minRotationX = -80f;
    public float maxRotationY = 80f; // Added maximum Y-axis rotation
    public float minRotationY = -80f; // Added minimum Y-axis rotation
    public float rotationSmoothness = 10f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        // Rotate the camera around the sitting point
        transform.RotateAround(sittingPoint.position, Vector3.up, mouseX);

        // Rotate the camera vertically with smoothing
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minRotationX, maxRotationX);

        // Rotate the camera horizontally with smoothing
        rotationY += mouseX;
        rotationY = Mathf.Clamp(rotationY, minRotationY, maxRotationY);

        Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * rotationSmoothness);
    }
}