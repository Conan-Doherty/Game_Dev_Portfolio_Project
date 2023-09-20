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
    float Speed = 2f;
    float currentSpeed;
    [SerializeField]
    InputActionReference Movement;
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
        
        Movement.action.Enable(); 
        
    }

    void OnDisable()
    {
       
        //Disables the character controlls action map
        
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
        this.transform.forward = currentMovement;
        characterController.Move(currentMovement * Time.deltaTime * currentSpeed);

    }
    // Update is called once per frame
    void Update()
    {
        MovementHandler();
    }
}
