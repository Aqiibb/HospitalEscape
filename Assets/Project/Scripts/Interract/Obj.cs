using System.Collections;
using UnityEngine;

public class Obj : MonoBehaviour
{
    private bool isBeingHeld = false;
    private Coroutine scalingCoroutine;
    private Coroutine rotationCoroutine;

    private void Start()
    {
        InteractiveObjectHandler InteractiveObjectHandler = FindObjectOfType<InteractiveObjectHandler>(); // Adjust as needed

        // Subscribe to the events
        InteractiveObjectHandler.OnObjectHeld += HandleObjectHeld;
        InteractiveObjectHandler.OnObjectDropped += HandleObjectDropped;
    }

    private void HandleObjectHeld(GameObject heldObject)
    {
        if (heldObject == gameObject)
        {
            Debug.Log($"{gameObject.name} is being held!");
            isBeingHeld = true;

            // Start scaling and rotating coroutines
            scalingCoroutine = StartCoroutine(ScaleObject(Vector3.one * 0.5f, 1f));
            rotationCoroutine = StartCoroutine(RotateObject(Vector3.up, 1f));

            // Change color on hold state
            ChangeColor(Color.green);
        }
    }

    private void HandleObjectDropped(GameObject heldObject)
    {
        if (heldObject == gameObject)
        {
            Debug.Log($"{gameObject.name} is dropped!");
            isBeingHeld = false;

            // Stop scaling and rotating coroutines
            if (scalingCoroutine != null)
                StopCoroutine(scalingCoroutine);

            if (rotationCoroutine != null)
                StopCoroutine(rotationCoroutine);

            // Reset the object size and rotation
            transform.localScale = Vector3.one;
            transform.rotation = Quaternion.identity;

            // Change color back to the original color
            ChangeColor(Color.white);
        }
    }

    private IEnumerator ScaleObject(Vector3 targetSize, float duration)
    {
        Vector3 initialSize = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration && isBeingHeld)
        {
            transform.localScale = Vector3.Lerp(initialSize, targetSize, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (isBeingHeld)
        {
            transform.localScale = targetSize;
        }
    }

    private IEnumerator RotateObject(Vector3 axis, float duration)
    {
        Quaternion initialRotation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration && isBeingHeld)
        {
            transform.rotation = Quaternion.AngleAxis(360f * (elapsedTime / duration), axis);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (isBeingHeld)
        {
            transform.rotation = initialRotation;
        }
    }

    private void ChangeColor(Color newColor)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = newColor;
        }
    }
}