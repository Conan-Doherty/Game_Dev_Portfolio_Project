using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerBehaviour : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    [SerializeField]
    float Speed = 2f;
    [SerializeField]
    float currentSpeed;
    [SerializeField]
    InputActionReference Movement;
    [SerializeField]
    float rotatespeed = 5f;
    float playervelocity = -1f;
    public GameObject shuriken;
    [SerializeField]
    float shurikenthrowdelay = 1f;
    float shurikendelayelapsedtime = 0f;
    [SerializeField]
    Transform throwpoint;
    public HealthBar healthbar;
    public DashBar dashbar;
    int Dashamount = 3;
   // public AudioSource pickup;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        
    }
    void OnEnable()
    {

        //Enables the character controlls action map
       // playerInput.CharacterControl.Enable();
        Movement.action.Enable(); 
        
    }

    void OnDisable()
    {

        //Disables the character controlls action map
      //  playerInput.CharacterControl.Disable();
        Movement.action.Disable();
       
    }
    void MovementHandler()
    {
        
        currentMovementInput = Movement.action.ReadValue<Vector2>();
        currentMovement = new Vector3(currentMovementInput.x, 0, currentMovementInput.y);
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;

        currentMovement.y = 0f;


        if (!isMovementPressed)
        {

            currentSpeed = 0f;
        }
        else
        {
            
            currentSpeed = Speed;
        }
        
        
        characterController.Move(currentMovement * Time.deltaTime * currentSpeed);
         
        
    }
    void OnDash()
    {
        if (Dashamount > 0 && Speed == 2f)
        {
            Speed = 8f;
            Dashamount--;
            StartCoroutine(resetspeed());
            StartCoroutine(dashregen());
            dashbar.setdashamount(Dashamount);
        }
    }
    void OnInteract()
    {
        Debug.Log("interact");
    }
    void OnShuriken()
    {
       
        if(GameManager.gameManager.itemscollected._currentammo > 0 && shurikendelayelapsedtime >= shurikenthrowdelay)
        {
            Debug.Log("shuriken");
            shurikendelayelapsedtime = 0;
            Playerdropammo();
            Instantiate(shuriken, throwpoint.position, throwpoint.rotation);
        }
        else
        {
            return;
        }
    }
    IEnumerator resetspeed()
    {
        yield return new WaitForSeconds(0.5f);
        Speed = 2f;
    }
    IEnumerator dashregen()
    {
        yield return new WaitForSeconds(6f);
        Dashamount++;
        dashbar.setdashamount(Dashamount);
    }
    void RotationHandler()
    {
        Vector3 positionToLookAt;
        //The change in position our character should point to
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        //Current rotation of character
        Quaternion currentRotationQuaternion = transform.rotation;

        if (isMovementPressed)
        {
            //Creates a new rotation based on where the player is currently pressing
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotationQuaternion, targetRotation, rotatespeed * Time.deltaTime);
        }
    }
    // Update is called once per frame
    void Update()
    {
        MovementHandler();
        RotationHandler();
        shurikendelayelapsedtime += Time.deltaTime;
    }
    public void PlayerPickupammo()
    {
        GameManager.gameManager.itemscollected.addammo();
        //pickup.PlayOneShot(pickup.clip, 0.5f);
    }
    public void Playerdropammo()
    {
        GameManager.gameManager.itemscollected.removeammo();
    }
    public void PlayerPickupintel()
    {
        GameManager.gameManager.itemscollected.addtreasure();
        //pickup.PlayOneShot(pickup.clip, 0.5f);
    }
    public void Playeraddkill()
    {
        GameManager.gameManager.itemscollected.addkill();
    }
    public void PlayerTakeDmg(int dmg)
    {
        GameManager.gameManager._PlayerHealth.dmgUnit(dmg);
        healthbar.sethealth(GameManager.gameManager._PlayerHealth.Health);
    }
    public void Playerhealdmg(int heal)
    {
        GameManager.gameManager._PlayerHealth.HealUnit(heal);
        healthbar.sethealth(GameManager.gameManager._PlayerHealth.Health);
       // pickup.PlayOneShot(pickup.clip, 0.5f);
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            PlayerTakeDmg(10);
            Playeraddkill();


        }
        if (other.gameObject.CompareTag("deadzone"))
        {
            PlayerTakeDmg(100);
        }
        if (other.gameObject.CompareTag("projectile"))
        {
            PlayerTakeDmg(25);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("ammo"))
        {
            PlayerPickupammo();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("treasure"))
        {
            PlayerPickupintel();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("goal"))
        {
            GameManager.gameManager.GoalReached();
        }
        if (other.gameObject.CompareTag("repair"))
        {
            Playerhealdmg(50);
            Destroy(other.gameObject);
        }
    }
}

