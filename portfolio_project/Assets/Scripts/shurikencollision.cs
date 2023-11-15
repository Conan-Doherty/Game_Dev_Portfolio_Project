using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shurikencollision : MonoBehaviour
{
    [SerializeField]
    float shurikendestroydelay = 2f;
    float shurikenelapsedtime = 0f;
    public GameObject thisone;
    public GameObject rotate;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotate.transform.Rotate(0, 1, 0);
        shurikenelapsedtime += Time.deltaTime;
        if(shurikenelapsedtime >= shurikendestroydelay)
        {
            Destroy(this.gameObject);
        }
    }
}
