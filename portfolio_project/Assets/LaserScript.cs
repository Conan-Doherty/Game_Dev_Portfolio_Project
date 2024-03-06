using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public Vector3 InPosition;
    public Vector3 OutPosition;

    public PlayerBehaviour player;
    public BossController bossManScript;

    [SerializeField] bool attacked;

    private Ray ray;
    private RaycastHit hit;
    [SerializeField] float attackTimer = 2f;
    [SerializeField] float returnTimer = 5f;
    [SerializeField] Transform shotPoint;

    private void Awake()
    {
        player = GameObject.Find("Model_Unity_Ver1").GetComponent<PlayerBehaviour>();

        bossManScript = GameObject.Find("Boss Parent").GetComponent<BossController>();

    }

    private void Start()
    {
        ray = new Ray(shotPoint.position, transform.forward);
    }
    void Update()
    {
        attackTimer -= Time.deltaTime;
        if (attacked == true)
        {
            returnTimer -= Time.deltaTime;
            if (returnTimer <= 0)
            {

            }
        }
        else if (attackTimer <= 0f && Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                if (player.isparrying)
                {
                    DestroyLaser(1); // move to "DestroyLaser" function
                }
                else
                {
                    player.PlayerTakeDmg(10); // directly calls the damage function within PlayerBehaviour
                }
            }
            attacked = true;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            DestroyLaser(1);
        }
    }
}
