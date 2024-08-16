using UnityEngine;
using UnityEngine.AI;

public class TileEnemy1 : MonoBehaviour
{
    public float attackRange = 1.5f;

    public Animator animator;
    public AnimationClip idleAnimation;
    public AnimationClip hitAnimation;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private bool isHit = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent != null)
        {
            navMeshAgent.updateRotation = false; // Prevent NavMeshAgent from rotating the enemy
        }
    }

    void Update()
    {
        if (!isHit)
        {
            // Set the destination to the player's position
            if (navMeshAgent != null)
            {
                navMeshAgent.SetDestination(player.position);
            }

            // Face the player
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Check if within attack range
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                // Attack the player
                Attack();
            }
            else
            {
                // Play idle animation if not attacking
                animator.Play(idleAnimation.name);
            }
        }
        else
        {
            // Play hit animation
            animator.Play(hitAnimation.name);
            // Reset hit flag after animation finishes
            Invoke("ResetHit", hitAnimation.length);

            // Debug statement for jump scare
            Debug.Log("Now playing jump scare!");
        }
    }

    void ResetHit()
    {
        isHit = false;
    }

    void Attack()
    {
        // Implement attack logic here
    }

    public void TakeHit()
    {
        // Set hit flag to true
        isHit = true;
    }
}