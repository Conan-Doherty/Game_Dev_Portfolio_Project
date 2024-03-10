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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!children[0].gameObject.activeSelf)
        {
            enemydoor.isdefended = false;
            enemydoor.opening();
        }
    }
}
