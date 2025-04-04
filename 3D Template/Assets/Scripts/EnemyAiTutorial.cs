using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.VisualScripting;

public class EnemyAiTutorial : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Transform Player;
    public LayerMask WhatIsGround, WhatIsPlayer;
    public GameObject Projectile;
    public float health;

    //patrol
    public Vector3 walkpoint;
    bool walkPointset;
    public float walkpointRange;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    private void Update()
    {
        ////check for sight and attack range
        PlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

        if (!PlayerInSightRange && !PlayerInAttackRange)
        {
            Patroling();
        }
        if (PlayerInSightRange)/* && !PlayerInAttackRange)*/
        {
            ChasePlayer();
        }
        //if (PlayerInSightRange && PlayerInAttackRange)
        //{
        //    AttackPlayer();
        //}
    }
    private void Awake()
    {
        Player = GameObject.Find("PlayerOBJ").transform;
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Patroling()
    {
        if (!walkPointset)
        { StartCoroutine(SearchWalkPoint()); }
        if (walkPointset)
        {
            Agent.SetDestination(walkpoint);

            Vector3 distanceToWalkPoint = transform.position - walkpoint;

            if (distanceToWalkPoint.magnitude < 1f)
            {
                walkPointset = false;
            }
        }
    }
    private void ChasePlayer()
    {
        Agent.SetDestination(Player.position);
    }

    //private void AttackPlayer()
    //{
    //    Agent.SetDestination(transform.position);
    //    transform.LookAt(Player);

    //    if (!alreadyAttacked) {
    //        Rigidbody rb = Instantiate(Projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
    //        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
    //        rb.AddForce(transform.up * 8f, ForceMode.Impulse);

    //        alreadyAttacked = true;
    //        Invoke(nameof(ResetAttack), timeBetweenAttacks);

    //    }
    //}
    //private void ResetAttack()
    //{
    //   alreadyAttacked = false;
    //}

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    private IEnumerator SearchWalkPoint()
    {

        float randomZ = Random.Range(-walkpointRange, walkpointRange);
        float randomX = Random.Range(-walkpointRange, walkpointRange);

        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkpoint, -transform.up, 2f, WhatIsGround))
        {
            walkPointset = true;
            yield return new WaitForSeconds(2);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        SearchWalkPoint();


        //video : 3:18
    }
}