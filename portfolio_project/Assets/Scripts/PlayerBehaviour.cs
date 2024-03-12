using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerBehaviour : MonoBehaviour//main player control script
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
    public bool isparrying = false;
    public bool isinteracting = false;
    public GameObject parryuiobj;
    public HealthBar healthbar;
    public DashBar dashbar;
    int Dashamount = 3;
    Animator animator;
    bool canparry = true;
    public Cinemachine.CinemachineVirtualCamera vcam;
    public Transform target;
    public GameObject swordobj;
    Collider swordcol;
    public AudioSource dashnoise;
    public AudioSource shingnoise;
    // public AudioSource pickup;
    // Start is called before the first frame update
    void Start()//grabs/creates instances on start
    {
        swordcol = swordobj.GetComponent<Collider>();
        swordcol.enabled = false;
        target = this.gameObject.transform;
        vcam.LookAt = target;
        vcam.Follow = target;
        Cursor.lockState = CursorLockMode.Locked;
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    void OnEnable()//enable inputs
    {

        //Enables the character controlls action map
       // playerInput.CharacterControl.Enable();
        Movement.action.Enable(); 
        
    }

    void OnDisable()//disable inputs
    {

        //Disables the character controlls action map
      //  playerInput.CharacterControl.Disable();
        Movement.action.Disable();
       
    }
    void MovementHandler()//handles input of movement data and translates it into movement and animation
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
    void OnDash()//alters speed if player is dashing and is able to
    {
        if (Dashamount > 0 && Speed == 6f)
        {
            animator.SetBool("Ismoving", true);
            animator.SetBool("IsDashing", true);
            dashnoise.Play();
            Speed = 16f;
            Dashamount--;
            StartCoroutine(resetspeed());
            StartCoroutine(dashregen());
            dashbar.setdashamount(Dashamount);

        }
    }
    void OnInteract()// interact function will check for locked doors for opening them
    {
        isinteracting = true;
        Debug.Log("interact");
        isinteracting= false;
    }
    void OnShuriken()// when shuriken button is pressed if the player still has some it increments down one and then creates a instance of one
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
    //below are several inumerators used for managing the values with specific inputs and functions as well as aiding animation
    IEnumerator resetspeed()
    {
        yield return new WaitForSeconds(0.5f);
        Speed = 6f;
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
        swordcol.enabled = false;
        animator.SetBool("IsAttacking", false);
        Speed = 6f;
    }
    IEnumerator ParryFinisher()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsParrying", false);
        Speed = 6f;
        yield return new WaitForSeconds(parrydelay);
        parryuiobj.SetActive(true);
        canparry = true;
        isparrying = false;
    }
    void OnAttack()
    {
        Debug.Log("Attack");
        animator.SetBool("IsAttacking", true);
        swordcol.enabled= true;
        Speed = 0f;
        StartCoroutine(Attackfinisher());
    }
    void OnParry()
    {
        if(canparry)
        {
            shingnoise.Play();
            isparrying = true;
            canparry= false;
            parryuiobj.SetActive(false);
            Debug.Log("Parry");
            animator.SetBool("IsParrying", true);
            Speed = 0f;
            StartCoroutine(ParryFinisher());
        }
        

    }
    void RotationHandler()//handles rotation input data for player so it rotates smoothly
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
    //below are several methods used for adding things to the item collector or health values 
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
    
    //below checks for collisions and if tag conditions are right will execute the code inside
    void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("deadzone"))
        {
            PlayerTakeDmg(100);
        }
        if (other.gameObject.CompareTag("projectile"))
        {
            if (isparrying)
            {

            }
            else
            {
                PlayerTakeDmg(5);
            }
           
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

        if (other.gameObject.CompareTag("repair"))
        {
            Playerhealdmg(50);
            Destroy(other.gameObject);
        }
        
    }
    IEnumerator camerapan(GameObject other)// this enumerator will be used to control camera changes 
    {
        Debug.Log("works");
        target = other.gameObject.transform.parent;
        DoorControler control = other.gameObject.GetComponentInParent<DoorControler>();
        control.opening();
        vcam.LookAt = target;
        vcam.Follow = target;
        yield return new WaitForSeconds(2);
        target = this.gameObject.transform;
        vcam.LookAt = target;
        vcam.Follow= target;
    }
    //same as above comment but with a trigger collider
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.CompareTag("uniquekey"))
        {
            Destroy(other.gameObject);
            StartCoroutine(camerapan(other.gameObject));
        }
        if (other.gameObject.CompareTag("goal"))
        {
            GameManager.gameManager.GoalReached();
        }
        if (other.gameObject.CompareTag("bossroomdoor"))
        {
            DoorControler gb = other.gameObject.GetComponent<DoorControler>();
            gb.isdefended = false;
            gb.opening();
            Transform r = other.transform.GetChild(0);
            vcam.LookAt = r;
            vcam.Follow = r;
        }
    }
}

