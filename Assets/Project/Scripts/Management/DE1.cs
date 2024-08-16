using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DE : MonoBehaviour
{
    public float minFocusDistance = 1f;
    public float maxFocusDistance = 10f;

    private DepthOfField depthOfField;

    private void Start()
    {
        // Get the DepthOfField component from the Post-Processing Volume attached to the camera
        depthOfField = GetComponent<PostProcessVolume>().profile.GetSetting<DepthOfField>();
    }

    private void Update()
    {
        // Calculate the average distance from the camera to all objects in the scene
        float totalDistance = 0f;
        int objectCount = 0;

        foreach (Renderer renderer in FindObjectsOfType<Renderer>())
        {
            float distance = Vector3.Distance(transform.position, renderer.bounds.center);
            totalDistance += distance;
            objectCount++;
        }

        if (objectCount > 0)
        {
            // Calculate the average distance
            float averageDistance = totalDistance / objectCount;

            // Map the average distance to a normalized value between 0 and 1
            float normalizedDistance = Mathf.InverseLerp(minFocusDistance, maxFocusDistance, averageDistance);

            // Update the focus distance of the DepthOfField component
            depthOfField.focusDistance.value = normalizedDistance;
        }
    }
}