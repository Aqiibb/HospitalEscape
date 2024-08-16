using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;
    public float health;
    public float walkPointRange;
    public float timeBetweenAttacks;
    public float sightRange;
    public float attackRange;
    public int damage;
    public Animator animator;
    public ParticleSystem hitEffect;

    private Vector3 walkPoint;
    private bool walkPointSet;
    private bool alreadyAttacked;
    private bool takeDamage;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        // player = GameObject.Find("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        // Check if the player reference is null
        if (player == null)
        {
            // If the player reference is null, find the player object again
            GameObject playerObject = GameObject.Find("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                // If the player object is still null, return from the update method
                return;
            }
        }

        // Now continue with your regular logic
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        else if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        else if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
        else if (!playerInSightRange && takeDamage)
        {
            ChasePlayer();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            navAgent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        animator.SetTrigger("Walk");

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        navAgent.SetDestination(player.position);
        animator.SetTrigger("Run"); // SetTrigger needs a parameter indicating which trigger to set
        navAgent.isStopped = false; // Ensure the NavMeshAgent is not stopped
    }


    private void AttackPlayer()
    {
        navAgent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            transform.LookAt(player.position);
            alreadyAttacked = true;
            animator.SetBool("Attack", true);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
            {
                /*
                    YOU CAN USE THIS TO GET THE PLAYER HUD AND CALL THE TAKE DAMAGE FUNCTION

                PlayerHUD playerHUD = hit.transform.GetComponent<PlayerHUD>();
                if (playerHUD != null)
                {
                   playerHUD.takeDamage(damage);
                }
                 */
            }
        }
    }


    private void ResetAttack()
    {
        alreadyAttacked = false;
        animator.SetBool("Attack", false);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        hitEffect.Play();
        StartCoroutine(TakeDamageCoroutine());

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private IEnumerator TakeDamageCoroutine()
    {
        takeDamage = true;
        yield return new WaitForSeconds(2f);
        takeDamage = false;
    }

    private void DestroyEnemy()
    {
        StartCoroutine(DestroyEnemyCoroutine());
    }

    private IEnumerator DestroyEnemyCoroutine()
    {
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}