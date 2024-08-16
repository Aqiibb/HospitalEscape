using UnityEngine;
public class ActivePanel : MonoBehaviour
{
    public GameObject panelToActivate;
    public void OnButtonClick()
    {
        panelToActivate.SetActive(true);
    }
}