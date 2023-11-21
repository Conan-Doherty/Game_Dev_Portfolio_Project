using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour
{
    
    [SerializeField]
    GameObject door1;
    [SerializeField]
    GameObject door2;
    [SerializeField]
    bool isclosing = false;
    [SerializeField]
    bool isopening = false;
    [SerializeField]
    float openclosedelay = 1.75f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        if (isclosing)
        {
            door1.transform.Translate(Vector3.right*Time.deltaTime);
            door2.transform.Translate(Vector3.left * Time.deltaTime);
        }
        if(isopening)
        {
            door1.transform.Translate(Vector3.left * Time.deltaTime);
            door2.transform.Translate(Vector3.right * Time.deltaTime);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(open());
           
          
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            StartCoroutine(close());
        }
    }
    IEnumerator open()
    {
        isopening= true;
        yield return new WaitForSeconds(openclosedelay);
        isopening= false;
    }
    IEnumerator close()
    {
        isclosing = true;
        yield return new WaitForSeconds(openclosedelay);
        isclosing= false;
    }
   
}
