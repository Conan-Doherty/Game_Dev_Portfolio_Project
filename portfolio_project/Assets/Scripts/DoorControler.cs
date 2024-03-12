using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour // doors lacked usable animations so i wrote this to compensate
{
    //serialized/public fields for the object references and other values for ease of interaction
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
    public bool islocked;
    public bool isuniquelock;
    public GameObject uniquekey;
    public bool isopen = false;
    public bool isdefended = false;
    public bool isbossroomdoor = false;
    public AudioSource a;
   
    // Start is called before the first frame update
    void Start()
    {
        a= GetComponent<AudioSource>();
        a.loop= false;
        a.playOnAwake= false;
       
    }

    // Update is called once per frame
    void Update()
    {


        //the following if statements check for player precense using the booleans and then open or close the doors as nessesary
        if (isclosing && isopen)
        {
            door1.transform.Translate(Vector3.right*Time.deltaTime);
            door2.transform.Translate(Vector3.left * Time.deltaTime);
        }
        if(isopening && !isopen)
        {
            door1.transform.Translate(Vector3.left * Time.deltaTime);
            door2.transform.Translate(Vector3.right * Time.deltaTime);
        }

    }
    void OnTriggerEnter(Collider other)// trigger detection checks for player keys if the door is set to locked but defaults to open if it isnt locked
    {
       
        if (isuniquelock)
        {
            if (!uniquekey)
            {

                if (!isopen)
                {
                    StartCoroutine(open());
                    isopen = true;
                }
            }
        }
        else if (isdefended) 
        {
            isopening= false;
            
        }
        else
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!isopen)
                {
                    StartCoroutine(open());
                    isopen = true;
                }
            }
        }
    }
   
    public void opening()
    {
        StartCoroutine(open());
    }
    public IEnumerator open()//times the opening
    {
        isopening= true;
        a.Play();
        yield return new WaitForSeconds(openclosedelay);
        a.Stop();
        isopen = true;
        isopening= false;
    }
    public void closing()
    {
        StartCoroutine(close());
    }
    IEnumerator close()//times the closing
    {
        if (isopen) 
        {
            isclosing = true;
            a.Play();
            yield return new WaitForSeconds(openclosedelay);
            a.Stop();
            isclosing = false;
            isopen= false;
        }
        
    }
   
}
