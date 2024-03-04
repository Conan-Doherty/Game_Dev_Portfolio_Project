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
        Debug.Log("works");
        target = this.gameObject.transform;
        vcam.LookAt = target;
        vcam.Follow = target;
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(Level);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(camerapan());
        }
        
    }

}
