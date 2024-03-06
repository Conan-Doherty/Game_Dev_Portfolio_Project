using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenchoDamage : MonoBehaviour
{
    public Vector3 InPosition; // position where the cassette (weakpoint) is not visible and will ignore damage
    public Vector3 OutPosition; // position where the cassette (weakpoint) is visible and will take damage
    public bool damagable; // decides whether or not the player can damage the cassette (weakpoint)

    public BossController mainScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DamagePhase()
    {
        transform.position = OutPosition;
        damagable = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (damagable == true && other.CompareTag("Sword"))
        {
            //set health here

            damagable = false;
            transform.position = InPosition;
            mainScript.damaged = true;
        }
    }
}
