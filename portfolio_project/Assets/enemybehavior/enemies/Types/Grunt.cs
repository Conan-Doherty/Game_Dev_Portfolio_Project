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
    public GameObject player;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject death;
    
    public NavMeshAgent agent; // This is for pathfinding

    private bool alreadyAttacked = false; // important for fire rate

    public Transform shotPoint; // where the bullet spawns
    public GameObject projectile; // The entire bullet

    public bool InSightRange = false; // for actions relating to being in sight range
    public bool InAttackRange = false; // same as above but for attack range
    public LayerMask playerlayer;
    public EnemyGetter stats; // input choice for switch case in EnemyGetter
    public Checker door;
    private void Awake()
    {
        door = GetComponentInParent<Checker>();
        stats = new EnemyGetter(choice);
        player = GameObject.Find("Model_Unity_Ver1");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.walkSpeed;
       

    }
    public void FixedUpdate()
    {
        InSightRange = Physics.CheckSphere(transform.position, stats.sightRange, playerlayer); // check for player inside view distance of unit
    }
    // Update is called once per frame
    private void Update()
    {
        if (InSightRange)
        {
            ChasePlayer();
        } // set unit to pathfind to player until within attack range
        // this is just a quick animation set up to make up for problems with the old models. (future me, write down magnitude because I know you'll forget)
       
        if (agent.velocity.magnitude > 0) 
        {
            
            anim.speed = 1;
        }
        else
        {
            anim.speed = 0;
        }
    }


    private void ChasePlayer()
    {

        
        agent.SetDestination(player.transform.position);

        transform.LookAt(player.transform);

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
            DestroyEnemy();
            Debug.Log("sword collided");
        }
    }

    private void DestroyEnemy()
    {
        //Instantiate(death);
        
        this.gameObject.SetActive(false);
       
        GameManager.gameManager.itemscollected.addkill();
        if (door != null)
        {
            door.deadnumber++;
            door.deadcheck();
        }

    }
}
