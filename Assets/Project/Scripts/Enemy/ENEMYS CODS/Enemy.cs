using UnityEngine;
using UnityEngine.AI;

public class TileEnemy : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public float attackRange = 1.5f;
    public LayerMask groundLayer;

    public Animator animator;
    public AnimationClip idleAnimation;
    public AnimationClip hitAnimation;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private bool isHit = false;

    private float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent != null)
        {
            navMeshAgent.updateRotation = false; // Prevent NavMeshAgent from rotating the enemy
        }

        timer = wanderTimer;
    }

    void Update()
    {
        if (!isHit)
        {
            // Check if there's ground under the enemy
            if (!IsGrounded())
            {
                // Stop movement if there's no ground
                if (navMeshAgent != null)
                {
                    navMeshAgent.isStopped = true;
                }
                return;
            }

            // Check if the player is within attack range
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                // Attack the player
                Attack();
            }
            else
            {
                // Check if it's time to wander
                timer += Time.deltaTime;
                if (timer >= wanderTimer)
                {
                    // Wander
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    if (navMeshAgent != null)
                    {
                        navMeshAgent.SetDestination(newPos);
                    }
                    timer = 0;
                }

                // Play idle animation if not attacking
                animator.Play(idleAnimation.name);

                // Start chasing the player if in sight
                RaycastHit hit;
                if (Physics.Raycast(transform.position, player.position - transform.position, out hit, Mathf.Infinity))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        // Chase the player
                        ChasePlayer();
                    }
                }
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

    void ChasePlayer()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.1f, groundLayer);
    }

    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    public void TakeHit()
    {
        // Set hit flag to true
        isHit = true;
    }
}