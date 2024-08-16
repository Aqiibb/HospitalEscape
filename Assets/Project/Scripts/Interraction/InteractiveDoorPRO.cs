using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace EvolveGames
{
    public class InteractiveDoorPRO : MonoBehaviour
    {
        [SerializeField] float openAngle = 90f;
        [SerializeField] float rotationSpeed = 90f;
        [SerializeField] float maxViewAngle = 30f;
        [SerializeField] float interactDistance = 3f;
        [SerializeField] float textDisplayDuration = 3f;
        [SerializeField] Camera cam;
        [SerializeField] Image imagePrefab;
        [SerializeField] string openText = "Press R to open";
        [SerializeField] string closeText = "Press R to close";

        public Transform hingePoint; // Assign the hinge point in the Inspector

        private bool isOpen = false;
        private Quaternion initialRotation;
        private Image image;
        private Text text;
        private Coroutine displayTextCoroutine;

        private void Start()
        {
            if (hingePoint == null)
            {
                Debug.LogError("Hinge point is not assigned for the door!");
                return;
            }

            initialRotation = hingePoint.rotation;

            // Create the image and text for displaying interact prompt
            image = Instantiate(imagePrefab, FindObjectOfType<Canvas>().transform);
            image.enabled = false;
            text = image.GetComponentInChildren<Text>();
            text.enabled = false;
        }

        private void Update()
        {
            if (hingePoint == null)
                return;

            Vector3 doorPosition = hingePoint.position;
            image.transform.position = cam.WorldToScreenPoint(calculateWorldPosition(doorPosition, cam));
            float distance = Vector3.Distance(cam.transform.position, doorPosition);

            if (distance <= interactDistance)
            {
                image.enabled = true;
                text.enabled = true;
                text.text = isOpen ? closeText : openText;

                if (displayTextCoroutine != null)
                    StopCoroutine(displayTextCoroutine);

                displayTextCoroutine = StartCoroutine(DisplayTextForDuration());
            }
            else
            {
                image.enabled = false;
                text.enabled = false;
            }

            if (distance <= interactDistance && IsCameraFacingDoor())
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (isOpen)
                        CloseDoor();
                    else
                        OpenDoor();
                }
            }
        }

        private bool IsCameraFacingDoor()
        {
            Vector3 cameraToDoor = hingePoint.position - cam.transform.position;
            float angle = Vector3.Angle(cameraToDoor, cam.transform.forward);

            return angle <= maxViewAngle;
        }

        private void OpenDoor()
        {
            isOpen = true;
            Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, openAngle, 0f);
            StartCoroutine(RotateDoor(targetRotation));
        }

        private void CloseDoor()
        {
            isOpen = false;
            StartCoroutine(RotateDoor(initialRotation));
        }

        private IEnumerator RotateDoor(Quaternion targetRotation)
        {
            while (Quaternion.Angle(hingePoint.rotation, targetRotation) > 0.01f)
            {
                hingePoint.rotation = Quaternion.RotateTowards(hingePoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                yield return null;
            }
        }

        private IEnumerator DisplayTextForDuration()
        {
            yield return new WaitForSeconds(textDisplayDuration);
            text.enabled = false;
            displayTextCoroutine = null;
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