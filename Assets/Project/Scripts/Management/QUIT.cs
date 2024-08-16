using UnityEngine;
public class QUIT : MonoBehaviour 
{
    public KeyCode quitKey = KeyCode.Escape;
    void Update()
    {
        if (Input.GetKeyDown(quitKey))
        {
            QuitGame();
        }
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}