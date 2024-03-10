using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grunt : MonoBehaviour
{
    public Animator anim;
    public bool isWalking;
    public bool isFiring;


    [Header ("choice 0 = grunt, 1 = burster")]
    public int choice;
    public EnemyGetter EnemyGetter;
    public Transform playerLocation;

    public NavMeshAgent agent; // This is for pathfinding

    private bool alreadyAttacked = false; // important for fire rate

    public Transform shotPoint; // where the bullet spawns
    public GameObject projectile; // The entire bullet

    public bool InSightRange; // for actions relating to being in sight range
    public bool InAttackRange; // same as above but for attack range

    public EnemyGetter stats; // input choice for switch case in EnemyGetter
    private void Awake()
    {
        stats = new EnemyGetter(choice);
        playerLocation = GameObject.Find("Model_Unity_Ver1").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.walkSpeed;

    }

    // Update is called once per frame
    private void Update()
    {
        
        InSightRange = Physics.CheckSphere(transform.position, stats.sightRange, 3); // check for player inside view distance of unit

        if (InSightRange)
        {
            ChasePlayer();
            anim.speed = 1;
        } // set unit to pathfind to player until within attack range
        else
        {
            anim.speed = 0;
        }      
    }


    private void ChasePlayer()
    {

        
        agent.SetDestination(playerLocation.position);

        transform.LookAt(playerLocation);

        Fire();
    }

    public void Fire()
    {
        
        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, shotPoint.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * stats.bulletSpeed, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), stats.attackSpeed);
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
            Invoke(nameof(DestroyEnemy), 0.5f);
            Debug.Log("sword collided");
        }
    }

    private void DestroyEnemy()
    {
        this.gameObject.SetActive(false);
    }
}
