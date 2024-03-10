using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject cassette;
    public SenchoDamage cassetteScript;

    public bool damaged = false;
    public bool stunned = false;
    public bool attacked = false;

    [SerializeField] GameObject particleOne;
    [SerializeField] GameObject particleTwo;
    [SerializeField] GameObject dead;
    [SerializeField] GameObject blue;

    [SerializeField] GameObject face;

    public GameObject laser;
    public GameObject key;
    public GameObject player;
    public GameObject dropPoint;
    public GameObject laserPoint;

    public bool side;

    [SerializeField] int damageTaken = 0;

    [SerializeField] float attackTimer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Model_Unity_Ver1");
        cassetteScript = cassette.GetComponent<SenchoDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer -= Time.deltaTime;
        if (damaged && stunned)
        {
            damageTaken += 1;
            
            ShowDamage(damageTaken);
            attackTimer = 3f;
            damaged = false;
            stunned = false;
        }
        
            if (attackTimer <= 0.0f)
            {
                if (!stunned)
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            SideAttack();
                            break;
                        case 1:
                            DropAttack();
                            break;
                    }
                
                
                }
            attackTimer = 3f;
        }



    }
    public void SideAttack()
    {
        attackTimer = 3f;
        if (side) // every time a side attack is made, it swaps sides
        {
            side = false;
            laserPoint.transform.position = new Vector3(67.06f, 11.62f, player.transform.position.z); // make laser line up with player
            laserPoint.transform.eulerAngles = new Vector3(0f, 0f, 0f); // rotate depending on wall
            Instantiate(laser, laserPoint.transform);
        }
        else
        {
            side = true;
            laserPoint.transform.position = new Vector3(95.85f, 11.62f, player.transform.position.z); // make laser line up with player
            laserPoint.transform.eulerAngles = new Vector3(0f, 0f, 180f); // rotate depending on wall
            Instantiate(laser, laserPoint.transform);
        }
        
        Debug.Log("side attack");

    }
    public void DropAttack()
    {
        attackTimer = 3f;
        dropPoint.transform.position = new Vector3(player.transform.position.x, 19.4f, player.transform.position.z); // drop key on player because funny
        dropPoint.transform.eulerAngles = new Vector3(0f, 0f, 0f); // this is just incase the sideattack changes anything

        Instantiate(key, dropPoint.transform);
        Debug.Log("drop attack");
        
    }
    public void DamageRotation()
    {
        face.SetActive(false);
        stunned = true;
        Debug.Log("damage rotation");
        cassetteScript.DamagePhase();

    }
    public void ShowDamage(int phase)
    {
        switch (phase)
        {
            case 1:
                Instantiate(particleOne, transform);
                face.SetActive(true);
                break;
            case 2:
                Instantiate(particleTwo, transform);
                face.SetActive(true);
                break;
            case 3:
                // insert escape stuff here
                dead.SetActive(true);
                blue.SetActive(true);
                Debug.Log("boss dead");
                break;
        }
    }
}
