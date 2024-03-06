using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Checker : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public NavMeshAgent[] children;
    public DoorControler enemydoor;
    void Start()
    {
        gameObject.GetComponentsInChildren<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (children.Length <= 0)
        {
            enemydoor.open();
        }
    }
}
