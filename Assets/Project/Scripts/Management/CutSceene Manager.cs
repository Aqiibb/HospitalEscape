using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutSceneManager : MonoBehaviour
{
    public VideoPlayer cutscenePlayer;
    private bool isInCutscene = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCutscene();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInCutscene)
        {
            if (!cutscenePlayer.isPlaying)
            {
                OnCutsceneFinished();
            }
        }
        else
        {
            // Game is not in cutscene, normal gameplay logic here
            // For example, player movement, enemy AI, etc.
        }
    }

    void StartCutscene()
    {
        // Pause the game while the cutscene is playing
        Time.timeScale = 0f;
    }

    // Call this method when the cutscene finishes to resume gameplay
    public void OnCutsceneFinished()
    {
        // Resume the game after the cutscene finishes
        Time.timeScale = 1f;
        isInCutscene = false;

        // Additional logic to start the game after the cutscene
        // For example, spawning the player, starting AI behavior, etc.
    }
}