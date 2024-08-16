using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EvolveGames
{
    public class ParticlePlayer : MonoBehaviour
    {
        [Header("Viewpoint")]
        [SerializeField] string PointText = "Press E";
        [Space]
        [SerializeField] Camera cam;
        [SerializeField] GameObject PlayerController;
        [SerializeField] Image ImagePrefab;
        [SerializeField] float MaxViewRange = 8;
        [SerializeField] float MaxTextViewRange = 3;
        [SerializeField] LayerMask interactableLayer;

        public ParticleSystem particleSystem; // The particle system to play
        public Light particleLight; // Optional light component to toggle

        private bool hasPlayed = false; // Indicates whether the particle system has been played

        private Text imageText;
        private Image imageUI;

        private void Start()
        {
            imageUI = Instantiate(ImagePrefab, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
            imageText = imageUI.GetComponentInChildren<Text>();
            imageText.text = PointText;
        }

        private void Update()
        {
            imageUI.transform.position = cam.WorldToScreenPoint(calculateWorldPosition(transform.position, cam));
            float distance = Vector3.Distance(PlayerController.transform.position, transform.position);

            if (distance < MaxTextViewRange)
            {
                Color opacityColor = imageText.color;
                opacityColor.a = Mathf.Lerp(opacityColor.a, 1, 10 * Time.deltaTime);
                imageText.color = opacityColor;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Interact();
                }
            }
            else
            {
                Color opacityColor = imageText.color;
                opacityColor.a = Mathf.Lerp(opacityColor.a, 0, 10 * Time.deltaTime);
                imageText.color = opacityColor;
            }

            if (distance < MaxViewRange)
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

        private void Interact()
        {
            // Toggle the state of hasPlayed
            hasPlayed = !hasPlayed;

            if (hasPlayed)
            {
                // Play the particle system if within interaction range
                float distance = Vector3.Distance(PlayerController.transform.position, transform.position);
                if (distance <= MaxTextViewRange)
                {
                    particleSystem.Play();
                }

                // Toggle the particle light if available
                if (particleLight != null)
                {
                    particleLight.enabled = true;
                }
            }
            else
            {
                // Stop the particle system
                particleSystem.Stop();

                // Toggle the particle light if available
                if (particleLight != null)
                {
                    particleLight.enabled = false;
                }
            }
        }
    }
}