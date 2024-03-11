using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Checker : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public GameObject[] children;
    public DoorControler enemydoor;
    bool alldead = false;
    int number;
    public int deadnumber = 0;
    void Start()
    {
        number = children.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void deadcheck()
    {
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].gameObject.activeSelf == false)
            {
                deadnumber++;
            }
        }
        if (deadnumber >= number)
        {
            enemydoor.isdefended = false;
            enemydoor.opening();
        }
    }
    
}
