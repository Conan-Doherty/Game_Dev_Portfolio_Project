using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }
    public UnitHealth _PlayerHealth = new UnitHealth(100, 100);
    public GameObject pausemenu;
    public GameObject deadmenu;
    public GameObject Winmenu;
    public GameObject player;
    public ItemCollector itemscollected = new ItemCollector(0, 0,3);
    public TextMeshProUGUI score;
  
    
    
    
    float completetime = 0f;
    public GameObject shuriken1, shuriken2, shuriken3;
    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.HasKey("kills"))
        {
            itemscollected._currentammo = PlayerPrefs.GetInt("ammo");
            itemscollected._currentkills = PlayerPrefs.GetInt("kills");
            itemscollected._currenttreasure = PlayerPrefs.GetInt("Treasure");
        }
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausegame();
        }
        if (_PlayerHealth.Health <= 0)
        {
            deadplayer();
        }
        shurikencounter();
        
        
        
       
       
        
       
        
        
        
    }
   
    void shurikencounter()
    {
        if (itemscollected._currentammo == 0)
        {
            shuriken1.SetActive(false);
            shuriken2.SetActive(false);
            shuriken3.SetActive(false);
        }
        else if(itemscollected._currentammo == 1)
        {
            shuriken1.SetActive(true);
            shuriken2.SetActive(false);
            shuriken3.SetActive(false);

        }
        else if(itemscollected._currentammo == 2)
        {
            shuriken1.SetActive(true);
            shuriken2.SetActive(true);
            shuriken3.SetActive(false);
        }
        else if(itemscollected._currentammo == 3)
        {
            shuriken1.SetActive(true);
            shuriken2.SetActive(true);
            shuriken3.SetActive(true);
        }
    }
    public void GoalReached()
    {
        Winmenu.SetActive(true);
        int Score = itemscollected._currentkills + itemscollected._currenttreasure;
        score.text = "Score: " + Score * 100;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;

    }
    public void deadplayer()
    {
        Time.timeScale = 0;
        deadmenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    public void startgame()
    {
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
    public void backtomain()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
    }
    public void pausegame()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        pausemenu.SetActive(true);
    }
    public void resumegame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        pausemenu.SetActive(false);
    }
    public void restartlevel(int choice)
    {
        //string currentscene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(choice);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

    }
    public void quitgame()
    {
        Application.Quit();
    }
}