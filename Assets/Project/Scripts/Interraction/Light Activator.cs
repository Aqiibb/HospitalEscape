using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightController : MonoBehaviour
{
    public List<Light> spotlights; // List of all the spotlights in the scene

    public void ActivateSpotlight(int spotlightIndex)
    {
        for (int i = 0; i < spotlights.Count; i++)
        {
            if (i == spotlightIndex)
            {
                // Activate the spotlight
                spotlights[i].gameObject.SetActive(true);
            }
            else
            {
                // Deactivate other spotlights
                spotlights[i].gameObject.SetActive(false);
            }
        }
    }
}

