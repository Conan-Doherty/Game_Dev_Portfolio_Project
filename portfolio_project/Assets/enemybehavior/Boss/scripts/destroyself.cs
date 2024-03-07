using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyself : MonoBehaviour
{
    [SerializeField] float destroyTimer;
    void Update()
    {
        destroyTimer -= Time.deltaTime;
        if (destroyTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
