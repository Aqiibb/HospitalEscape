using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DynamicDepthOfField : MonoBehaviour
{
    public LayerMask focusLayerMask; // Layer mask for objects to consider for focusing
    public float maxBlurSize = 10f; // Maximum blur size when an object is close to the camera
    public float minBlurSize = 0f; // Minimum blur size when there are no nearby objects
    public float maxFocusDistance = 50f; // Maximum focus distance
    public float minFocusDistance = 1f; // Minimum focus distance
    public float damping = 5f; // Damping factor for smoother transitions
    public float lerpSpeed = 5f; // Lerp speed for smoother transitions

    private Camera mainCamera; // Reference to the main camera
    private PostProcessVolume postProcessVolume; // Reference to the Post Process Volume
    private DepthOfField depthOfField; // Reference to the Depth of Field effect settings
    private float targetBlurSize; // Target blur size for lerping
    private float targetFocusDistance; // Target focus distance for lerping

    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }

        // Get the Post Process Volume component
        postProcessVolume = mainCamera.GetComponent<PostProcessVolume>();
        if (postProcessVolume == null)
        {
            Debug.LogError("Post Process Volume component not found!");
            return;
        }

        // Get the Depth of Field effect settings
        if (!postProcessVolume.profile.TryGetSettings(out depthOfField))
        {
            Debug.LogError("Depth of Field settings not found!");
            return;
        }

        // Initialize target values
        targetBlurSize = minBlurSize;
        targetFocusDistance = maxFocusDistance;
    }

    private void Update()
    {
        if (mainCamera == null || postProcessVolume == null || depthOfField == null)
        {
            return;
        }

        // Perform a sphere cast around the camera to detect nearby objects
        Collider[] colliders = Physics.OverlapSphere(mainCamera.transform.position, maxFocusDistance, focusLayerMask);

        if (colliders.Length > 0)
        {
            // Find the closest object to the camera
            float closestDistance = Mathf.Infinity;
            foreach (Collider collider in colliders)
            {
                float distance = Vector3.Distance(mainCamera.transform.position, collider.bounds.ClosestPoint(mainCamera.transform.position));
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                }
            }

            // Calculate the blur size based on the distance to the closest object
            float normalizedDistance = Mathf.Clamp01(closestDistance / maxFocusDistance);
            float blurSize = Mathf.Lerp(minBlurSize, maxBlurSize, normalizedDistance);

            // Set the depth of field focus distance based on the distance to the closest object
            float focusDistance = Mathf.Lerp(minFocusDistance, maxFocusDistance, normalizedDistance);

            // Smoothly transition to the new focus distance and blur size
            targetFocusDistance = Mathf.Lerp(targetFocusDistance, focusDistance, Time.deltaTime * lerpSpeed);
            targetBlurSize = Mathf.Lerp(targetBlurSize, blurSize, Time.deltaTime * lerpSpeed);
        }
        else
        {
            // If there are no nearby objects, set the depth of field settings to default
            targetFocusDistance = maxFocusDistance;
            targetBlurSize = minBlurSize;
        }

        // Apply the smoothed focus distance and blur size
        depthOfField.focusDistance.value = Mathf.Lerp(depthOfField.focusDistance.value, targetFocusDistance, Time.deltaTime * damping);
        depthOfField.aperture.value = Mathf.Lerp(depthOfField.aperture.value, targetBlurSize, Time.deltaTime * damping);
    }

    public void SetMaxFocusDistance(float distance)
    {
        maxFocusDistance = distance;
    }

    public void SetMinFocusDistance(float distance)
    {
        minFocusDistance = distance;
    }

    public void SetMaxBlurSize(float size)
    {
        maxBlurSize = size;
    }

    public void SetMinBlurSize(float size)
    {
        minBlurSize = size;
    }
}