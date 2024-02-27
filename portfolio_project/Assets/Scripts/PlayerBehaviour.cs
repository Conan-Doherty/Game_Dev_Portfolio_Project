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
    float Speed = 4f;
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
    [SerializeField]
    float parrydelay = 2f;
    float parrydelayelapsedtime = 0f;
    public GameObject parryuiobj;
    public HealthBar healthbar;
    public DashBar dashbar;
    int Dashamount = 3;
    Animator animator;
    bool canparry = true;
   // public AudioSource pickup;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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
            animator.SetBool("Ismoving", false);
            currentSpeed = 0f;
        }
        else
        {
            animator.SetBool("Ismoving", true);
            currentSpeed = Speed;
        }
        
        
        characterController.Move(currentMovement * Time.deltaTime * currentSpeed);
         
        
    }
    void OnDash()
    {
        if (Dashamount > 0 && Speed == 4f)
        {
            animator.SetBool("Ismoving", true);
            animator.SetBool("IsDashing", true);
            Speed = 16f;
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
        Speed = 4f;
        animator.SetBool("IsDashing", false);
    }
    IEnumerator dashregen()
    {
        yield return new WaitForSeconds(6f);
        Dashamount++;
        dashbar.setdashamount(Dashamount);
    }
    IEnumerator Attackfinisher()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsAttacking", false);
        Speed = 4f;
    }
    IEnumerator ParryFinisher()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsParrying", false);
        Speed = 4f;
        yield return new WaitForSeconds(parrydelay);
        parryuiobj.SetActive(true);
        canparry = true;
    }
    void OnAttack()
    {
        Debug.Log("Attack");
        animator.SetBool("IsAttacking", true);
        Speed = 0f;
        StartCoroutine(Attackfinisher());
    }
    void OnParry()
    {
        if(canparry)
        {
            canparry= false;
            parryuiobj.SetActive(false);
            Debug.Log("Parry");
            animator.SetBool("IsParrying", true);
            Speed = 0f;
            StartCoroutine(ParryFinisher());
        }
        

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
       // parrydelayelapsedtime += Time.deltaTime;
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
        if (other.gameObject.CompareTag("Laser"))
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

