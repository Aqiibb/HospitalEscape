using UnityEngine;

public class TVController : MonoBehaviour, IInteractable
{
    public Renderer tvScreenRenderer;
    public RenderTexture tvRenderTexture;
    public Material defaultMaterial;

    private bool isTVOn = false;

    // Implement the Interact method from the IInteractable interface
    public void Interact()
    {
        // Toggle the TV state when interacting
        ToggleTVState();
    }

    void ToggleTVState()
    {
        // Toggle the TV state
        isTVOn = !isTVOn;

        // Update the state of the TV
        SetTVState(isTVOn);
    }

    void SetTVState(bool tvOn)
    {
        // Change the material of the TV screen renderer based on the TV state
        if (tvOn)
        {
            // Use the material with the Render Texture when the TV is on
            tvScreenRenderer.material.mainTexture = tvRenderTexture;
        }
        else
        {
            // Use the default material when the TV is off
            tvScreenRenderer.material = defaultMaterial;
        }

        // Debug log to check the TV state
        Debug.Log("TV is " + (tvOn ? "on" : "off"));
    }

    // Update is called once per frame
    void Update()
    {
        // You can add additional logic here if needed
    }
}