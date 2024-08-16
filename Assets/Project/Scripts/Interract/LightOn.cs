using UnityEngine;

public class LightToggle : MonoBehaviour, IInteractable
{
    public Light controlledLight;
    public bool isLightOn = false;

    // Implement the Interact method from the IInteractable interface
    public void Interact()
    {
        // Toggle the light state when interacting
        ToggleLightState();
    }

    void ToggleLightState()
    {
        // Toggle the light state
        isLightOn = !isLightOn;

        // Update the state of the controlled light
        SetLightState(isLightOn);
    }

    void SetLightState(bool lightOn)
    {
        // Activate/deactivate the controlled light based on the light state
        controlledLight.enabled = lightOn;
    }

    // Update is called once per frame
    void Update()
    {
        // You can add additional logic here if needed
    }
}