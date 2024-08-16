using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CupBottun : MonoBehaviour
{
    public int cupIndex; // The index of the cup associated with this button
    public CupGame cupGame; // Reference to the CupGame script

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectCup);
    }

    private void SelectCup()
    {
        // Disable the button to prevent multiple selections
        button.interactable = false;

        StartCoroutine(SelectCupCoroutine());
    }

    private IEnumerator SelectCupCoroutine()
    {
        // Wait for a short delay to allow the cups to finish shuffling
        yield return new WaitForSeconds(0.5f);

        bool foundHiddenObject = cupGame.CheckSelectedCup(cupIndex);

        // Perform any actions based on the result
        if (foundHiddenObject)
        {
            // The player found the hidden object
            Debug.Log("You found the hidden object!");
        }
        else
        {
            // The player didn't find the hidden object
            Debug.Log("Sorry, try again!");
        }

        // Wait for a brief moment to allow the player to see the result
        yield return new WaitForSeconds(1f);

        // Enable the button again for the next selection
        button.interactable = true;
    }
}