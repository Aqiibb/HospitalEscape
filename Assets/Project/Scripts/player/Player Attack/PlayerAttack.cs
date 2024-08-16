using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f; // Range within which the player can attack
    public int damage = 10; // Damage dealt by the player's attack
    public LayerMask enemyLayer; // Layer mask for the enemy

    public GameObject weapon; // Reference to the weapon object
    public ParticleSystem hitEffect; // Particle effect for the attack

    private Animator weaponAnimator; // Animator attached to the weapon
    private CameraShake cameraShake; // Reference to the camera shake script

    private void Start()
    {
        // Get the Animator component from the weapon object
        weaponAnimator = weapon.GetComponent<Animator>();
        // Find the CameraShake script attached to the main camera
        cameraShake = Camera.main.GetComponent<CameraShake>();

        // Perform an initial attack on start
        Attack();
    }

    private void Update()
    {
        // Check if the player is attacking
        if (Input.GetButtonDown("Fire1")) // Change "Fire1" to your attack input button
        {
            // Trigger the attack animation on the weapon
            weaponAnimator.SetTrigger("Attack");
            // Perform the attack
            Attack();
        }
    }

    private void Attack()
    {
        // Play hit effect at the position of the attack
        hitEffect.transform.position = transform.position + transform.forward * attackRange;
        hitEffect.Play();

        // Trigger camera shake effect
        cameraShake.Shake();

        // Perform a raycast to detect enemies within the attack range
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, attackRange, enemyLayer);

        // Debug draw the raycast
        Debug.DrawRay(transform.position, transform.forward * attackRange, Color.red, 0.5f);

        // Log the number of hits
        Debug.Log("Number of hits: " + hits.Length);

        // Apply damage to each enemy hit by the raycast
        foreach (RaycastHit hit in hits)
        {
            // Check if the hit object has an Enemy component
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Apply damage to the enemy
                enemy.TakeDamage(damage);

                // Log a message indicating that damage was dealt to the enemy
                Debug.Log("Dealt " + damage + " damage to " + enemy.gameObject.name);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the attack range in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}