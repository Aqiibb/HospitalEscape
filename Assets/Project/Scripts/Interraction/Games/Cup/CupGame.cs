using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupGame : MonoBehaviour
{
    public GameObject[] cups; // Array of cup game objects
    public GameObject hiddenObject; // The object hidden underneath the cups

    private int correctCupIndex; // Index of the cup containing the hidden object

    private void Start()
    {
        ShuffleCups();
    }

    // Shuffle the positions of the cups
    private void ShuffleCups()
    {
        int cupCount = cups.Length;

        // Randomly swap the cups' positions
        for (int i = 0; i < cupCount; i++)
        {
            int randomIndex = Random.Range(0, cupCount);
            Vector3 tempPosition = cups[i].transform.position;
            cups[i].transform.position = cups[randomIndex].transform.position;
            cups[randomIndex].transform.position = tempPosition;

            // Set the correct cup index
            if (i == 0)
            {
                correctCupIndex = randomIndex;
            }
            else if (randomIndex == 0)
            {
                correctCupIndex = i;
            }
        }
    }

    // Check if the selected cup contains the hidden object
    public bool CheckSelectedCup(int selectedIndex)
    {
        if (selectedIndex == correctCupIndex)
        {
            // The player guessed correctly
            Debug.Log("You found the hidden object!");
            return true;
        }
        else
        {
            // The player guessed incorrectly
            Debug.Log("Sorry, try again!");
            return false;
        }
    }
}

