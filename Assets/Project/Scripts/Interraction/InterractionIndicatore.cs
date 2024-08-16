using UnityEngine;
using UnityEngine.UI;

namespace EvolveGames
{
    public class InteractionIndicator : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private GameObject playerController;
        [SerializeField] private Image imagePrefab;
        [SerializeField] private string pointText = "Press E";
        [SerializeField] private float maxViewRange = 8f;
        [SerializeField] private float maxTextViewRange = 3f;

        private Text imageText;
        private Image imageUI;

        private void Start()
        {
            imageUI = Instantiate(imagePrefab, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
            imageText = imageUI.GetComponentInChildren<Text>();
            imageText.text = pointText;
        }

        private void Update()
        {
            imageUI.transform.position = cam.WorldToScreenPoint(calculateWorldPosition(transform.position, cam));
            float distance = Vector3.Distance(playerController.transform.position, transform.position);

            if (distance < maxTextViewRange)
            {
                Color opacityColor = imageText.color;
                opacityColor.a = Mathf.Lerp(opacityColor.a, 1, 10 * Time.deltaTime);
                imageText.color = opacityColor;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    SendMessageUpwards("Interact", SendMessageOptions.DontRequireReceiver);
                }
            }
            else
            {
                Color opacityColor = imageText.color;
                opacityColor.a = Mathf.Lerp(opacityColor.a, 0, 10 * Time.deltaTime);
                imageText.color = opacityColor;
            }

            if (distance < maxViewRange)
            {
                Color opacityColor = imageUI.color;
                opacityColor.a = Mathf.Lerp(opacityColor.a, 1, 10 * Time.deltaTime);
                imageUI.color = opacityColor;
            }
            else
            {
                Color opacityColor = imageUI.color;
                opacityColor.a = Mathf.Lerp(opacityColor.a, 0, 10 * Time.deltaTime);
                imageUI.color = opacityColor;
            }
        }

        private Vector3 calculateWorldPosition(Vector3 position, Camera camera)
        {
            Vector3 camNormal = camera.transform.forward;
            Vector3 vectorFromCam = position - camera.transform.position;
            float camNormDot = Vector3.Dot(camNormal, vectorFromCam.normalized);
            if (camNormDot <= 0f)
            {
                float camDot = Vector3.Dot(camNormal, vectorFromCam);
                Vector3 proj = (camNormal * camDot * 1.01f);
                position = camera.transform.position + (vectorFromCam - proj);
            }
            return position;
        }
    }
}