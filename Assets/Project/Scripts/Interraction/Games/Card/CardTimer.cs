using UnityEngine;
using TMPro;

public class CardTimer : MonoBehaviour
{
    public float totalTime = 60f; // Total time in seconds
    public TextMeshProUGUI timerText;

    private float currentTime;
    private bool isTimerRunning;
    private CardFlip cardFlip;

    private void Start()
    {
        currentTime = totalTime;
        isTimerRunning = true;
        cardFlip = GetComponent<CardFlip>();
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isTimerRunning = false;
                // Add any logic you want when the timer is over
            }
        }

        if (!cardFlip.IsFlipping && currentTime <= 0f)
        {
            cardFlip.StartFlip();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
