using UnityEngine;
using TMPro;
using System.Collections;

public class FlickerTextMeshPro : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float minAlpha = 0.2f; // Minimum alpha value for flickering
    public float maxAlpha = 1f; // Maximum alpha value for flickering
    public float flickerSpeed = 1f; // Speed of flickering

    private bool isFlickering = false;

    void Start()
    {
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI component is not assigned!");
            enabled = false; // Disable script if TextMeshProUGUI component is not assigned
            return;
        }

        // Start the flickering effect
        StartFlicker();
    }

    void StartFlicker()
    {
        isFlickering = true;
        StartCoroutine(Flicker());
    }

    void StopFlicker()
    {
        isFlickering = false;
    }

    IEnumerator Flicker()
    {
        while (isFlickering)
        {
            float alpha = Random.Range(minAlpha, maxAlpha);
            textMeshPro.alpha = alpha;

            yield return new WaitForSeconds(1f / flickerSpeed);
        }
    }
}