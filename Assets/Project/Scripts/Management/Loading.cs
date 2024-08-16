using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] private GameObject transitionCanvasPrefab;
    [SerializeField] private Animator transitionSignAnimator; // Animator for sign animation

    private bool isTransitioning;

    public void StartTransition(string sceneName, float delay = 0f)
    {
        if (isTransitioning)
        {
            Debug.LogError("Transition already in progress!");
            return;
        }

        isTransitioning = true;
        StartCoroutine(TransitionCoroutine(sceneName, delay));
    }

    IEnumerator TransitionCoroutine(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Instantiate and activate transition canvas
        GameObject transitionCanvas = Instantiate(transitionCanvasPrefab);
        transitionCanvas.transform.SetParent(transform, false); // Set parent without affecting local transform
        transitionCanvas.SetActive(true);

        // Play sign animation (if animator is assigned)
        if (transitionSignAnimator != null)
        {
            transitionSignAnimator.SetTrigger("StartAnimation"); // Replace "StartAnimation" with your actual trigger name
        }

        // Load scene asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // Update loading UI elements here (optional)

            yield return null;
        }

        // On scene load complete
        Destroy(transitionCanvas);
        isTransitioning = false;
    }
}