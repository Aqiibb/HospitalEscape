using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneActivator : MonoBehaviour
{
    public string sceneName;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            ActivateScene();
        }
    }

    public void ActivateScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

