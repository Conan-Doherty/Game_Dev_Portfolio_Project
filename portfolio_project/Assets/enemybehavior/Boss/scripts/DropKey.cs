using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropKey : MonoBehaviour
{
    PlayerBehaviour player;
    private void Awake()
    {
        player = GameObject.Find("Model_Unity_Ver1").GetComponent<PlayerBehaviour>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.PlayerTakeDmg(10);
        }
        Destroy(gameObject);
    }
}
