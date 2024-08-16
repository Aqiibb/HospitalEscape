using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contacteme : MonoBehaviour
{
    public string urlToOpen; // Public variable to set the URL in the Inspector

    public void OpenUrl()
    {
        Application.OpenURL(urlToOpen);
    }

    void Start()
    {
        // Optional: Get the Button component if not directly attached in the Inspector
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OpenUrl); // Add listener to call OpenUrl on button click
        }
    }
}