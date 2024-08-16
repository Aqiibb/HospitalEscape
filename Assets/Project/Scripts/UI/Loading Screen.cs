using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;


public class LoadingScreen : MonoBehaviour
{
    public Image panel;
    public float fadeDuration = 1f;
    public float targetBrightness = 0f;

    private AsyncOperation loadingOperation;

    public void LoadSceneOnClick(string sceneName)
    {
        gameObject.SetActive(true);
        panel.gameObject.SetActive(true);

        // Fade in the panel
        panel.CrossFadeAlpha(targetBrightness, fadeDuration, true);

        Time.timeScale = 0f; // Pause the game while loading

        loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingOperation.allowSceneActivation = false; // Delay scene activation until progress reaches 90%

        StartCoroutine(UpdateProgressBar());
    }

    private IEnumerator UpdateProgressBar()
    {
        while (true)
        {
            float progress = Mathf.Clamp01(loadingOperation.progress / 0.9f);

            if (progress >= 0.9f)
            {
                // Fade out the panel
                panel.CrossFadeAlpha(0f, fadeDuration, true);

                loadingOperation.allowSceneActivation = true; // Activate the scene when progress reaches 90%

                yield return new WaitForSecondsRealtime(fadeDuration);

                panel.gameObject.SetActive(false); // Deactivate the panel after fading out
                Time.timeScale = 1f; // Resume the game

                break;
            }

            yield return null;
        }
    }
}
