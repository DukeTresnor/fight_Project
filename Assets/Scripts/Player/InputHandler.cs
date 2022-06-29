using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    // My Input Actions window is called PlayerInputActions, as opposed to PlayerMovement (that's what it is in fuckaroundadillo)

    private Player player;
    public Vector2 rawMoveInput {get; private set;}

    // Bools
    public bool movementInput {get; private set;}



    public bool isGrounded = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        rawMoveInput = context.ReadValue<Vector2>();

        Debug.Log("input handler Move was called");

        if (context.started)
        {
            movementInput = true;

            Debug.Log("Context true");
            Debug.Log(movementInput);
        }

        if (context.canceled)
        {
            movementInput = false;

            Debug.Log("Context false");
        }
    }

}
