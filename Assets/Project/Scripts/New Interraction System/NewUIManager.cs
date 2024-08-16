using UnityEngine;
using UnityEngine.UI;

public class NewUIManager : MonoBehaviour
{
    public static NewUIManager Instance { get; private set; }

    public Text interactionPromptText;
    public GameObject interactionPrompt;

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowInteractionPrompt(string promptText, string key, Vector3 position)
    {
        // Update the text
        interactionPromptText.text = $"{promptText} ({key})";
        interactionPrompt.SetActive(true);

        // Position the prompt in the world space
        interactionPrompt.transform.position = position;
    }

    public void HideInteractionPrompt()
    {
        interactionPrompt.SetActive(false);
    }
}