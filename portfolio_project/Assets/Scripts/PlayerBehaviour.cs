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
        
        currentMovement.y = -1f;

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
        Speed = 8f;
        StartCoroutine(resetspeed());
    }
    void OnInteract()
    {
        Debug.Log("interact");
    }
    void OnShuriken()
    {
        Debug.Log("shuriken");
    }
    IEnumerator resetspeed()
    {
        yield return new WaitForSeconds(0.5f);
        Speed = 2f;
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
    }
    
}
