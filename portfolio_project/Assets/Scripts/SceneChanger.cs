using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour // this script changes the level upon collision
{
    public int Level;
    public Cinemachine.CinemachineVirtualCamera vcam;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator camerapan()// this enumerator will be used to control camera changes 
    {
        PlayerPrefs.SetInt("kills", GameManager.gameManager.itemscollected.Kills);
        PlayerPrefs.SetInt("Treasure", GameManager.gameManager.itemscollected.Treasure);
        PlayerPrefs.SetInt("ammo", GameManager.gameManager.itemscollected.Ammo);
        Debug.Log("Chongle");
        target = this.gameObject.transform;
        vcam.LookAt = target;
        vcam.Follow = target;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(Level);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(camerapan());
        }
        
    }

}
