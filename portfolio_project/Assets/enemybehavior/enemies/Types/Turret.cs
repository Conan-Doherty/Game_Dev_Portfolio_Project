using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public EnemyGetter stats;
    public Transform playerLocation;

    private bool alreadyAttacked = false; // important for fire rate

    public Transform shotPoint; // where the bullet spawns
    public GameObject projectile; // The entire bullet
    public LayerMask playerlayer;
    [SerializeField] GameObject death;

    public bool InSightRange = false; // for actions relating to being in sight range
    public bool InAttackRange = false; // same as above but for attack range

    void Start()
    {
        stats = new EnemyGetter(2); // input choice for switch case

        playerLocation = GameObject.Find("Model_Unity_Ver1").transform;

    }

    // Update is called once per frame
    private void Update()
    {
        InSightRange = Physics.CheckSphere(transform.position, stats.sightRange, playerlayer); // check for player inside attack distance of unit
        if (InSightRange) AttackPlayer(); // set unit to fire at player
    }

    private void AttackPlayer()
    {

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
        Instantiate(death);
        GameManager.gameManager.itemscollected.addkill();
        this.gameObject.SetActive(false);
    }
}
