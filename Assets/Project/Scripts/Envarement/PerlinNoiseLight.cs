using UnityEngine;

public class PerlinNoiseLight : MonoBehaviour
{
    public Light targetLight;
    public float intensityNoiseFrequency = 1f;
    public float intensityNoiseAmplitude = 0.1f;
    public Renderer targetRenderer;
    public Color emissionColor = Color.white;
    public float emissionNoiseFrequency = 1f;
    public float emissionNoiseAmplitude = 0.1f;

    private float randomOffset;

    void Start()
    {
        randomOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        // Noise for intensity
        float intensityNoise = Mathf.PerlinNoise(Time.time * intensityNoiseFrequency + randomOffset, 0f);
        float intensity = 1f - intensityNoise * intensityNoiseAmplitude;
        targetLight.intensity = intensity;

        // Noise for emission
        float emissionNoise = Mathf.PerlinNoise(Time.time * emissionNoiseFrequency + randomOffset, 0f);
        Color finalEmissionColor = emissionColor * (1f - emissionNoise * emissionNoiseAmplitude);
        targetRenderer.material.SetColor("_EmissionColor", finalEmissionColor);
    }
}