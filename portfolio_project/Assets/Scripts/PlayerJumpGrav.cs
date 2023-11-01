using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerJumpGrav : MonoBehaviour
{
    private float height = 2.5f;
    private Vector3 playervelocity;
    [SerializeField]
    private float gravity = -1f;
    private bool canjump;

    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        
        
        playervelocity.y += gravity * Time.deltaTime;
        characterController.Move(playervelocity * Time.deltaTime);
    }
    
    
    //velocity.y += Mathf.Sqrt(height * -3.0f * gravity);
    //currentMovement.y -= gravity * Time.deltaTime;
    //characterController.Move(velocity * Time.deltaTime);
}