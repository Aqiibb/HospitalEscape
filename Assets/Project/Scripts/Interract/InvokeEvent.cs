using UnityEngine;
using Cinemachine;

public class InvokeEvent : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource impulseSource;
    public float impulseDuration = 0.1f; // Customize this value

    public float timeBetweenImpulses = 2f; // Customize this value
    private float timeSinceLastImpulse;

    private void Start()
    {
        // Initialize the timer
        timeSinceLastImpulse = timeBetweenImpulses;
    }

    private void Update()
    {
        // Timer for generating impulses
        timeSinceLastImpulse += Time.deltaTime;

        // Check if it's time to generate another impulse
        if (timeSinceLastImpulse >= timeBetweenImpulses)
        {
            GenerateImpulse();
            timeSinceLastImpulse = 0f; // Reset the timer
        }
    }

    private void GenerateImpulse()
    {
        // Generate an impulse
        impulseSource.GenerateImpulse();

        // You can also customize the impulse duration
        impulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = impulseDuration;
    }
}