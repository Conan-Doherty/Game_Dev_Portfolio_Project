using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

    public PlayerBehaviour player;
    public BossController bossManScript;

    [SerializeField] bool attacked;
    [SerializeField] GameObject laser;
    [SerializeField] destroyself laserScript;
    [SerializeField] GameObject funnyEffect;

    [SerializeField] float attackTimer = 5f;
    [SerializeField] float returnTimer = 0f;
    [SerializeField] Transform shotPoint;

    private void Awake()
    {
        player = GameObject.Find("Model_Unity_Ver1").GetComponent<PlayerBehaviour>();

        bossManScript = GameObject.Find("Boss Parent").GetComponent<BossController>();

    }

    void Update()
    {
        returnTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 3f && attackTimer > 0f) // checks if the laser is about to fire to create telegraph
        {
            funnyEffect.SetActive(true);
        }
        else
        {
            funnyEffect.SetActive(false);
        }
        if (attackTimer <= 0f && !attacked) // checks if laser countdown is finished and fires laser
        {
            laser.SetActive(true);

            returnTimer = 5f;
            attacked = true;
        }
        if (laserScript.hit && player.isparrying) // checks if laser got parried
        {
            DestroyLaser(1);
        }
        if (returnTimer <= 0f && attacked == true) // destroys itself if the player takes too long
        {
            DestroyLaser(0);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Sword")) // wanted the player to have an extra option to trigger damage phase if they don't know they can deflect or aren't used to it
        {
            DestroyLaser(1);
        }
    }

    void DestroyLaser(int method) // Destroys the laser and opens the boss up to a counter attack
    {
        if(method == 1)
        {
            bossManScript.DamageRotation();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.right) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }
}
