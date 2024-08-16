using UnityEngine;

public class CardFlip : MonoBehaviour
{
    public float flipForce = 100f; // The force to apply on the Y-axis when flipping.
    public float flipCooldown = 1f; // Cooldown time between flips in seconds.
    public string cardTag = "Card";
    public bool IsFlipping { get; private set; } // Property to check if the card is currently flipping.
    private bool isJumping = false;
    private bool isFlipped = false;
    private float jumpHeight = 0.3f;
    private Rigidbody RB;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (IsFlipping)
        {
            if (!isFlipped)
            {
                transform.rotation = Quaternion.Euler(180f, 0f, 0f);
                isFlipped = true;
            }

            ApplyFlipForce(); // Apply force when flipping.

            if (!isJumping)
            {
                isJumping = true;
                PerformJump();
            }

            if (RB.velocity.y <= 0f)
            {
                IsFlipping = false;
                isJumping = false;
                StartCoroutine(FlipCooldown());
            }
        }
        else if (Mathf.Abs(transform.rotation.eulerAngles.x) == 180f)
        {
            StartFlip();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag(cardTag) && hit.collider.gameObject == gameObject)
                {
                    if (!IsFlipping)
                    {
                        StartFlip();
                    }
                }
            }
        }
    }

    private void ApplyFlipForce()
    {
        Vector3 flipForceVector = new Vector3(0f, flipForce, 0f);
        RB.AddForce(flipForceVector);
    }

    public void StartFlip()
    {
        if (!IsFlipping)
        {
            IsFlipping = true;
            isFlipped = false;
        }
    }

    private void PerformJump()
    {
        float jumpForce = CalculateJumpForce();
        RB.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    private float CalculateJumpForce()
    {
        float jumpForce = Mathf.Sqrt(2f * Physics.gravity.magnitude * jumpHeight);
        return jumpForce;
    }

    private System.Collections.IEnumerator FlipCooldown()
    {
        yield return new WaitForSeconds(flipCooldown);
        if (Mathf.Abs(transform.rotation.eulerAngles.x) == 180f)
        {
            StartFlip();
        }
    }
}

