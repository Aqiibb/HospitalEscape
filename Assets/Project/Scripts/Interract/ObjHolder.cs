using System.Collections;
using UnityEngine;
using System;

public class InteractiveObjectHandler : MonoBehaviour
{
    public float throwForce = 10f;
    public LayerMask interactableLayer;
    public Vector3 holdSize = new Vector3(1.5f, 1.5f, 1.5f); // Size when held
    public Transform holdPoint; // The point where the object will be held
    public float scalingDuration = 0.5f; // Duration of scaling
    public float stickiness = 0.1f; // How much the object sticks to the hold point

    public GameObject hitIndicatorPrefab; // Prefab for hit indicator sphere

    private GameObject heldObject;
    private Vector3 holdOffset;
    private Camera playerCamera;
    private bool isHolding = false;
    private Vector3 originalSize;

    // Events for holding and dropping an object
    public event Action<GameObject> OnObjectHeld;
    public event Action<GameObject> OnObjectDropped;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHolding)
                TryPickUpObject();
            else
                DropObject();
        }

        if (isHolding)
        {
            MoveHeldObject();
            if (Input.GetMouseButtonDown(0))
                ThrowObject();
        }
    }

    void TryPickUpObject()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
        {
            heldObject = hit.collider.gameObject;
            holdOffset = hit.point - heldObject.transform.position;
            isHolding = true;

            // Disable rigidbody to avoid physics interactions
            Rigidbody objectRigidbody = heldObject.GetComponent<Rigidbody>();
            if (objectRigidbody != null)
                objectRigidbody.isKinematic = true;

            // Save the original size of the object
            originalSize = heldObject.transform.localScale;

            // Trigger the object held event
            InvokeObjectHeldEvent(heldObject);

            // Stick the object to the hold point
            heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, holdPoint.position, stickiness);
            StartCoroutine(ScaleObject(holdSize));

            // Instantiate hit indicator sphere
            Instantiate(hitIndicatorPrefab, hit.point, Quaternion.identity);
        }
    }

    void MoveHeldObject()
    {
        if (heldObject != null)
        {
            // Stick the object to the hold point
            heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, holdPoint.position, stickiness);
        }
    }

    IEnumerator ScaleObject(Vector3 targetSize)
    {
        float elapsedTime = 0f;

        while (elapsedTime < scalingDuration && heldObject != null)
        {
            heldObject.transform.localScale = Vector3.Lerp(originalSize, targetSize, elapsedTime / scalingDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object reaches the exact target size if it's not null
        if (heldObject != null)
        {
            heldObject.transform.localScale = targetSize;

            // Explicitly wait for the duration to complete
            yield return new WaitForSeconds(scalingDuration);
        }
    }

    void ThrowObject()
    {
        if (heldObject != null)
        {
            Rigidbody objectRigidbody = heldObject.GetComponent<Rigidbody>();
            if (objectRigidbody != null)
            {
                // Calculate throw direction
                Vector3 throwDirection = playerCamera.transform.forward;

                // Apply throw force
                objectRigidbody.AddForce(throwDirection * throwForce, ForceMode.Impulse);

                // Drop the object
                DropObject();
            }
        }
    }

    void DropObject()
    {
        if (heldObject != null)
        {
            // Re-enable rigidbody
            Rigidbody objectRigidbody = heldObject.GetComponent<Rigidbody>();
            if (objectRigidbody != null)
                objectRigidbody.isKinematic = false;

            // Reset the size of the object to its original size
            StartCoroutine(ScaleObject(originalSize));

            // Trigger the object dropped event
            InvokeObjectDroppedEvent(heldObject);

            heldObject = null;
            isHolding = false;
        }
    }

    // Expose methods to invoke events
    public void InvokeObjectHeldEvent(GameObject obj)
    {
        OnObjectHeld?.Invoke(obj);
    }

    public void InvokeObjectDroppedEvent(GameObject obj)
    {
        OnObjectDropped?.Invoke(obj);
    }
}
