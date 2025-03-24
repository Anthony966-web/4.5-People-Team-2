using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiTutorial : MonoBehaviour
{
public NavMeshAgent Agent;
public  Transform Player;
    public LayerMask WhatIsGround, WhatIsPlayer;

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
        //check for sight and attack range
        PlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

        if (!PlayerInSightRange && !PlayerInAttackRange) {
            Patroling();
        }
        if (PlayerInSightRange && !PlayerInAttackRange)
        {
            ChasePlayer();
        }
        if (PlayerInSightRange && PlayerInAttackRange)
        {
            AttackPlayer();
        }
    }
    private void Awake()
    {
        Player = GameObject.Find("PlayerOBJ").transform;
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Patroling()
    {
        if (!walkPointset)
        { SearchWalkPoint(); }
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
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkpointRange, walkpointRange);
        float randomX = Random.Range(-walkpointRange, walkpointRange);

        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkpoint, -transform.up, 2f, WhatIsGround)) {
            walkPointset = true;
        }
    }
    private void ChasePlayer()
    {

    }
    
    private void AttackPlayer()
    {

    }
    //video : 3:18
}
