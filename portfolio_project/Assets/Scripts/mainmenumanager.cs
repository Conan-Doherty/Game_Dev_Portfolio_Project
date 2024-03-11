using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainmenumanager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startgame(int choice)
    {
        SceneManager.LoadScene(choice);
    }
    public void leavegame()
    {
        Application.Quit();
    }
}
