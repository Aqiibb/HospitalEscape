using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFade : MonoBehaviour
{
    public float fadeDuration = 2f;
    public Color fadeColor = Color.black;

    private Image overlayImage;

    private void Start()
    {
        overlayImage = gameObject.AddComponent<Image>();
        overlayImage.rectTransform.SetParent(transform);
        overlayImage.rectTransform.SetAsLastSibling();
        overlayImage.color = fadeColor;

        // Start the fade-out effect
        StartCoroutine(FadeOut());
    }

    private System.Collections.IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            overlayImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Disable the overlay once the fade-out is complete
        overlayImage.enabled = false;
    }
}