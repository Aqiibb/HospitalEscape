using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeListener : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // Make sure the slider is properly initialized.
        if (volumeSlider != null)
        {
            // Set the initial value of the slider based on the current audio listener volume.
            volumeSlider.value = AudioListener.volume;

            // Subscribe to the slider's value changed event.
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }
    }

    void ChangeVolume(float volume)
    {
        // Adjust the audio listener volume based on the slider value.
        AudioListener.volume = volume;
    }
}