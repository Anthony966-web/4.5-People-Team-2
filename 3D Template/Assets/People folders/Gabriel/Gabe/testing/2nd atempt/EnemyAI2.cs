using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI2 : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform player;
    public float sightRange = 10f;
    public float attackRange = 2f;
    public float health = 100f;
    public float maxHealth = 100f;
    public float fleeDistance = 15f;
    public float attackCooldown = 1.5f;

    private NavMeshAgent agent;
    private int currentPatrolIndex = 0;
    private bool isFleeing = false;
    private float lastAttackTime = -999f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToNextPatrolPoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Low health = flee
        if (health < maxHealth * 0.3f)
        {
            FleeFromPlayer();
            return;
        }

        // Attack
        if (distanceToPlayer <= attackRange)
        {
            agent.SetDestination(transform.position);
            AttackPlayer();
        }
        // Chase
        else if (distanceToPlayer <= sightRange)
        {
            agent.SetDestination(player.position);
        }
        // Patrol
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

     public void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return ;

        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Debug.Log("Enemy attacks!");
            // Insert damage to player here
            lastAttackTime = Time.time;
        }
    }

    void FleeFromPlayer()
    {
        Vector3 fleeDirection = (transform.position - player.position).normalized;
        Vector3 fleeTarget = transform.position + fleeDirection * fleeDistance;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(fleeTarget, out hit, fleeDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }

        Debug.Log("Enemy is fleeing!");
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Enemy took " + amount + " damage!");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died.");
        Destroy(gameObject);
    }
}

