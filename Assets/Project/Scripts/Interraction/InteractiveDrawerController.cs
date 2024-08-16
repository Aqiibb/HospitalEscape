using UnityEngine;
using UnityEngine.UI;

namespace EvolveGames
{
    public class InteractiveDrawerController : MonoBehaviour
    {
        [Header("Viewpoint")]
        [SerializeField] string PointText = "Press R";
        [Space]
        [SerializeField] Camera cam;
        [SerializeField] GameObject PlayerController;
        [SerializeField] Image ImagePrefab;
        [SerializeField] float MaxViewRange = 8;
        [SerializeField] float MaxTextViewRange = 3;
        [SerializeField] LayerMask interactableLayer;

        public Transform drawerTransform;
        public float openDistance = 1f;
        public float openSpeed = 1f;

        private Vector3 closedPosition;
        private Vector3 openPosition;
        private bool isOpen = false;

        private Vector3 originalImagePosition;
        private Text imageText;
        private Image imageUI;

        private void Start()
        {
            closedPosition = drawerTransform.position;
            openPosition = closedPosition + drawerTransform.forward * openDistance;

            imageUI = Instantiate(ImagePrefab, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
            imageText = imageUI.GetComponentInChildren<Text>();
            imageText.text = PointText;

            originalImagePosition = imageUI.transform.position;
        }

        private void Update()
        {
            imageUI.transform.position = cam.WorldToScreenPoint(calculateWorldPosition(transform.position, cam));
            float distance = Vector3.Distance(PlayerController.transform.position, transform.position);

            // Toggle visibility based on distance from the player
            if (distance > MaxViewRange)
            {
                imageUI.gameObject.SetActive(false);
                return;
            }
            else
            {
                imageUI.gameObject.SetActive(true);
            }

            if (distance < MaxTextViewRange)
            {
                // Fade in the text if within interaction range
                Color opacityColor = imageText.color;
                opacityColor.a = Mathf.Lerp(opacityColor.a, 1, 10 * Time.deltaTime);
                imageText.color = opacityColor;

                // Check for interaction input
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Interact();
                }
            }
            else
            {
                // Fade out the text if outside interaction range
                Color opacityColor = imageText.color;
                opacityColor.a = Mathf.Lerp(opacityColor.a, 0, 10 * Time.deltaTime);
                imageText.color = opacityColor;
            }

            // Fade in/out the image based on distance
            Color imageOpacityColor = imageUI.color;
            imageOpacityColor.a = Mathf.Lerp(imageOpacityColor.a, 1 - Mathf.InverseLerp(MaxTextViewRange, MaxViewRange, distance), 10 * Time.deltaTime);
            imageUI.color = imageOpacityColor;

            // Handle drawer movement
            if (isOpen)
            {
                drawerTransform.position = Vector3.MoveTowards(drawerTransform.position, openPosition, openSpeed * Time.deltaTime);
                if (Vector3.Distance(drawerTransform.position, openPosition) < 0.001f)
                {
                    // Drawer is fully open, additional actions can be added here
                }
            }
            else
            {
                drawerTransform.position = Vector3.MoveTowards(drawerTransform.position, closedPosition, openSpeed * Time.deltaTime);
                if (Vector3.Distance(drawerTransform.position, closedPosition) < 0.001f)
                {
                    // Drawer is fully closed, additional actions can be added here
                }
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

        private void Interact()
        {
            if (isOpen)
            {
                // Close the drawer
                isOpen = false;
            }
            else
            {
                // Open the drawer if within interaction range
                float distance = Vector3.Distance(PlayerController.transform.position, transform.position);
                if (distance <= MaxTextViewRange)
                {
                    isOpen = true;
                }
            }
        }
    }
}