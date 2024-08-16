using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HV : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    public float intensity = 1f;
    public float speed = 1f;

    private ChromaticAberration chromaticAberration;
    private Grain grain;
    private Vignette vignette;

    private void Start()
    {
        postProcessVolume.profile.TryGetSettings(out chromaticAberration);
        postProcessVolume.profile.TryGetSettings(out grain);
        postProcessVolume.profile.TryGetSettings(out vignette);
    }

    private void Update()
    {
        // Animate the intensity of the visual effects
        float t = Mathf.PingPong(Time.time * speed, 1f);
        float effectIntensity = Mathf.Lerp(0f, intensity, t);

        // Adjust the settings of the post-processing effects
        chromaticAberration.intensity.value = effectIntensity;
        grain.intensity.value = effectIntensity;
        vignette.intensity.value = effectIntensity;
    }
}