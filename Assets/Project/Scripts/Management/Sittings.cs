using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;

    void Start()
    {
        // Make sure the settings panel is initially inactive.
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Check for the ESC key press.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the settings panel.
            ToggleSettingsPanel();
        }
    }

    void ToggleSettingsPanel()
    {
        if (settingsPanel != null)
        {
            // Toggle the settings panel's activation state.
            settingsPanel.SetActive(!settingsPanel.activeSelf);

            // You can add additional logic here, such as pausing the game when the settings panel is active.
        }
    }
}