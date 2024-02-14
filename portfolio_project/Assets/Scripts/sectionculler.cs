using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sectionculler : MonoBehaviour
{
    public GameObject first;
    public GameObject second;
    public GameObject blocker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(second.activeSelf)
        {
            second.SetActive(false);
        }
        else
        {
            second.SetActive(true);
            blocker.SetActive(true);
        }
        if (first.activeSelf)
        {
            first.SetActive(false);
        }
        else
        {
            first.SetActive(true);
            blocker.SetActive(false);
        }
    }
}
