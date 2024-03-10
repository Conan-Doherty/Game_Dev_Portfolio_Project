using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

    public PlayerBehaviour player;
    public BossController bossManScript;

    [SerializeField] bool attacked;
    [SerializeField] GameObject laser;

    bool inside = false;
    [SerializeField] float attackTimer = 2f;
    [SerializeField] float returnTimer = 5f;
    [SerializeField] Transform shotPoint;

    private void Awake()
    {
        player = GameObject.Find("Model_Unity_Ver1").GetComponent<PlayerBehaviour>();

        bossManScript = GameObject.Find("Boss Parent").GetComponent<BossController>();

    }

    void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f && !attacked)
        {
             if (bossManScript.side)
            {
                Instantiate(laser, transform.position, Quaternion.Euler(0f, 270f, 0f));
            }
            else
            {
                Instantiate(laser, transform.position, Quaternion.Euler(0f, 90f, 0f));
            }
            

            if (player.isparrying)
                {
                    Debug.Log("deflected laser");
                    DestroyLaser(1); // move to "DestroyLaser" function
                }
            Debug.Log("laser hit");

            attacked = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player")) // wanted the player to have an extra option to trigger damage phase if they don't know they can deflect or aren't used to it
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
