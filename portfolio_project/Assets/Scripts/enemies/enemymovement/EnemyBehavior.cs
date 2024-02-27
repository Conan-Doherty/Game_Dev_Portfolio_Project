using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;


    //patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public int timeout = 1000;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public Transform shotPoint;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [Header("CLICK TO TURN OFF ENEMY MOVEMENT")]
    public bool movement;

    private void Awake()
    {
        player = GameObject.Find("Model_Unity_Ver1").transform; // player must be called "Player" in order to work as intended
        agent = GetComponent<NavMeshAgent>();

    }


    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); // check for player inside view distance of unit
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); // check for player inside attack distance of unit
        if (movement)
        {
            if (!playerInSightRange && !playerInAttackRange) Patrolling(); // set unit to patrol area

            if (playerInSightRange && !playerInAttackRange) ChasePlayer(); // set unit to pathfind to player until within attack range
        }
        if (playerInAttackRange && playerInSightRange) AttackPlayer(); // set unit to fire at player

        timeout -= 1; // reset patrol if stuck

    }

    private void SearchWalkPoint()
    {
        // Return random values based on value of walkPointRange, effectively making a sphere around the unit in which it will randomly choose a point to reach
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) // checks walkPoint is safe ground that the unit can stand on. if it passes, the unit can move to the point, otherwise it checks for a new position.
        {
            walkPointSet = true;
        }
    }



    private void Patrolling()
    {
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
            {
                agent.SetDestination(walkPoint);


            }

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            // Walk Point Reached
            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
            if (timeout <= 0) // if the timeout reaches 0, find a new point and try again
            {
                SearchWalkPoint();
                timeout = 1000;
            }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position); // ensures the enemy does not continue to follow the player when within range

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, shotPoint.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            TakeDamage(1);
            Debug.Log("sword collided");
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("damage taken");
        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private void DestroyEnemy()
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
    
}
