using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyself : MonoBehaviour
{
    [SerializeField] float destroyTimer;
    public bool hit;
    void Update()
    {
        destroyTimer -= Time.deltaTime;
        if (destroyTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hit = true;
        }
    }
}
