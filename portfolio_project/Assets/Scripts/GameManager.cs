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
    public ItemCollector itemscollected = new ItemCollector(0, 0, 0);
    public TextMeshProUGUI score;
    public TextMeshProUGUI Kills;
    public TextMeshProUGUI treasurecollected;
    public TextMeshProUGUI score2;
    public TextMeshProUGUI Ammo;
    public TextMeshProUGUI Keys;
    public TextMeshProUGUI Timetaken;
    // Start is called before the first frame update
    void Awake()
    {
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
        Kills.text = "" + itemscollected._currentkills;
        treasurecollected.text = "" + itemscollected._currenttreasure;
        Keys.text = "" + itemscollected._currentkeys;
        score.text = "Score: " + (itemscollected._currentkills * itemscollected._currenttreasure) * 100;
        score2.text = "Score: " + (itemscollected._currentkills * itemscollected._currenttreasure) * 100;
        Ammo.text = "" + itemscollected._currentammo;
        Timetaken.text = "" + Time.deltaTime;
    }
    public void GoalReached()
    {
        Winmenu.SetActive(true);
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
    public void restartlevel()
    {
        //string currentscene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

    }
    public void quitgame()
    {
        Application.Quit();
    }
}