using UnityEngine;
using UnityEngine.UI;

public class RadioController2 : MonoBehaviour
{
    [Header("Viewpoint")]
    [SerializeField] string pointText = "Press E";
    [Space]
    [SerializeField] Camera cam;
    [SerializeField] GameObject playerController;
    [SerializeField] GameObject imagePrefab;
    [SerializeField] float maxViewRange = 8;
    [SerializeField] float maxTextViewRange = 3;
    [SerializeField] float fadeOutDistanceThreshold = 2;

    public AudioClip[] songs;
    private AudioSource audioSource;
    private int currentSongIndex = 0;
    private bool isRadioOn = true;

    private GameObject imageUI;
    private Text imageText;

    public delegate void RadioStateChangedEventHandler(bool isRadioOn);
    public event RadioStateChangedEventHandler OnRadioStateChanged;

    private void Start()
    {
        imageUI = Instantiate(imagePrefab, Object.FindFirstObjectByType<Canvas>().transform);
        imageText = imageUI.GetComponentInChildren<Text>();
        imageText.text = pointText;

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = songs[currentSongIndex];

        if (isRadioOn)
        {
            audioSource.Play();
            OnRadioStateChanged?.Invoke(true);
        }
    }

    private void Update()
    {
        imageUI.transform.position = cam.WorldToScreenPoint(calculateWorldPosition(transform.position, cam));
        float distance = Vector3.Distance(playerController.transform.position, transform.position);

        // Toggle visibility based on distance
        if (distance > maxViewRange)
        {
            imageUI.SetActive(false);
            return;
        }
        else
        {
            imageUI.SetActive(true);
        }

        float alpha = Mathf.Lerp(1f, 0f, Mathf.InverseLerp(fadeOutDistanceThreshold, maxTextViewRange, distance));
        imageText.color = new Color(imageText.color.r, imageText.color.g, imageText.color.b, alpha);

        if (Input.GetKeyDown(KeyCode.E) && distance <= maxTextViewRange)
        {
            Interact();
        }

        if (imageUI.GetComponent<Renderer>() != null)
        {
            Color opacityColor = imageUI.GetComponent<Renderer>().material.color;
            opacityColor.a = Mathf.Lerp(opacityColor.a, 1, 10 * Time.deltaTime);
            imageUI.GetComponent<Renderer>().material.color = opacityColor;
        }
        else
        {
            Debug.Log("ViewpointImage(Clone) is missing a Renderer component!");
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
            PlayNextSong();
        }
        else
        {
            audioSource.Stop();
        }

        isRadioOn = !isRadioOn;
        OnRadioStateChanged?.Invoke(isRadioOn);
    }

    private void PlayNextSong()
    {
        currentSongIndex = (currentSongIndex + 1) % songs.Length;
        audioSource.clip = songs[currentSongIndex];
        audioSource.Play();
    }
}