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
                switch (Random.Range(0, 1))
                {
                    case 0:
                        SideAttack();
                        break;
                    case 1:
                        DropAttack();
                        break;
                }
                attackTimer = 3f;
                attacked = true;
            }
        }



    }
    public void ResetTimer()
    {
        attackTimer = 10f;
    }
    public void SideAttack()
    {
        if (side) // every time a side attack is made, it swaps sides
        {
            side = false;
            dropPoint.transform.position = new Vector3(14.44f, 0.625f, player.transform.position.z); // make laser line up with player
            dropPoint.transform.eulerAngles = new Vector3(0f, 0f, 270f); // rotate depending on wall
        }
        else
        {
            side = true;
            dropPoint.transform.position = new Vector3(-14.469f, 0.625f, player.transform.position.z); // make laser line up with player
            dropPoint.transform.eulerAngles = new Vector3(0f, 0f, 90f); // rotate depending on wall
        }
        Instantiate(laser, dropPoint.transform);
    }
    public void DropAttack()
    {

        dropPoint.transform.position = new Vector3(player.transform.position.x, 8f, player.transform.position.z); // drop key on player because funny
        dropPoint.transform.eulerAngles = new Vector3(0f, 0f, 90f); // this is just incase the sideattack changes anything

        Instantiate(key, dropPoint.transform);

        
    }
    public void DamageRotation()
    {
        cassetteScript.DamagePhase();
    }
}
