using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EvolveGames
{
    public class RadioController : MonoBehaviour
    {
        [Header("Viewpoint")]
        [SerializeField] string PointText = "Press E";
        [Space]
        [SerializeField] Camera cam;
        [SerializeField] GameObject playerController;
        [SerializeField] Image imagePrefab;
        [SerializeField] float maxViewRange = 8;
        [SerializeField] float maxTextViewRange = 3;

        public AudioClip[] songs; // Array of songs to play
        private AudioSource audioSource; // Reference to the AudioSource component
        private int currentSongIndex = 0; // Index of the currently playing song
        private bool isRadioOn = true; // Indicates if the radio is currently on

        private Text imageText;
        private Image imageUI;

        private void Start()
        {
            imageUI = Instantiate(imagePrefab, Object.FindFirstObjectByType<Canvas>().transform).GetComponent<Image>();
            imageText = imageUI.GetComponentInChildren<Text>();
            imageText.text = PointText;

            audioSource = GetComponent<AudioSource>();
            audioSource.clip = songs[currentSongIndex];
            audioSource.Play();
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
                    Interact();
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

        private void Interact()
        {
            if (isRadioOn)
            {
                // Turn off the radio
                audioSource.Stop();
                isRadioOn = false;
            }
            else
            {
                // Turn on the radio and play the next song
                isRadioOn = true;
                PlayNextSong();
            }
        }

        private void PlayNextSong()
        {
            currentSongIndex = (currentSongIndex + 1) % songs.Length;
            audioSource.clip = songs[currentSongIndex];
            audioSource.Play();
        }
    }
}