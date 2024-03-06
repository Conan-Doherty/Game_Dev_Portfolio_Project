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

    public GameObject laser;
    public GameObject key;
    public GameObject player;
    public GameObject dropPoint;

    public bool side;

    [SerializeField] float attackTimer = 10f;
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
            damaged = false;
        }
        else if (!stunned)
        {
            if (attackTimer <= 0.0f)
            {
                attacked = true;
                switch (Random.Range(0, 2))
                {
                    case 0:
                        SideAttack();
                        break;
                    case 1:
                        DropAttack();
                        break;
                }
                attackTimer = 10f;
                
            }
        }



    }
    public void SideAttack()
    {
        attackTimer = 10f;
        if (side) // every time a side attack is made, it swaps sides
        {
            side = false;
            dropPoint.transform.position = new Vector3(67.06f, 11.62f, player.transform.position.z); // make laser line up with player
            dropPoint.transform.eulerAngles = new Vector3(0f, 0f, 0f); // rotate depending on wall
            Instantiate(laser, dropPoint.transform);
        }
        else
        {
            side = true;
            dropPoint.transform.position = new Vector3(95.85f, 11.62f, player.transform.position.z); // make laser line up with player
            dropPoint.transform.eulerAngles = new Vector3(0f, 0f, 180f); // rotate depending on wall
            Instantiate(laser, dropPoint.transform);
        }
        
        Debug.Log("side attack");

    }
    public void DropAttack()
    {
        attackTimer = 10f;
        dropPoint.transform.position = new Vector3(player.transform.position.x, 19.4f, player.transform.position.z); // drop key on player because funny
        dropPoint.transform.eulerAngles = new Vector3(0f, 0f, 0f); // this is just incase the sideattack changes anything

        Instantiate(key, dropPoint.transform);
        Debug.Log("drop attack");
        
    }
    public void DamageRotation()
    {
        cassetteScript.DamagePhase();
    }
}
