using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenchoDamage : MonoBehaviour
{
    public bool damagable; // decides whether or not the player can damage the cassette (weakpoint)

    public BossController mainScript;

    bool moving;
    bool inside;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (moving && inside) 
        {
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        else if (moving && !inside)
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }
            
        
    }
    public void DamagePhase()
    {
        inside = false;
        StartCoroutine(Open());
        Debug.Log("damage phase");
        damagable = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (damagable == true && other.CompareTag("Player"))
        {
            //set health here
            mainScript.damaged = true;
            damagable = false;
            inside = true;
            StartCoroutine(Open());
        }
    }
    public IEnumerator Open()//times the opening
    {
        moving = true;
        yield return new WaitForSeconds(2);
        moving = false;
    }
}
