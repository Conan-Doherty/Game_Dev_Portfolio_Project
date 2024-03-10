using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telegraph : MonoBehaviour
{
    [SerializeField]List<GameObject> prefabList;
    GameObject key;
    float resetTime = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
        key = prefabList[Random.Range(0, 5)];

        Instantiate(key, transform.position, Quaternion.identity);
        
    }
    private void Update()
    {
        resetTime -= Time.deltaTime;
        if (resetTime <= 0f) Destroy(gameObject);
    }
}
