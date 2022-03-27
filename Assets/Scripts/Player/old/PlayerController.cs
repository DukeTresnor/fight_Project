using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// reference here for now -- https://www.youtube.com/watch?v=g_s0y5yFxYg&ab_channel=samyam



[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;



    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;


    // Variables to keep track of using movement and jump functions, respectively
    // Add additional parameters here to handle additional action types
    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;



    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Callback function that is called when we trigger a movement input
    public void OnMove(InputAction.CallbackContext context)
    {
        // movementInput is set to context's value, which is being read as a Vector2 type
        movementInput = context.ReadValue<Vector2>();
    }

    // Callback function that is called when we trigger a jump input
    public void OnJump(InputAction.CallbackContext context)
    {
        //jumped = context.ReadValue<bool>();
        // context.action.triggered returns a true if it's been triggered on that frame -- ie OnJump should be called everyframe??
        jumped = context.action.triggered;
    }











    // Maybe use fixed update??? See when it's better...
    // Put logic in update, put the things that an input action does in their own functions
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // no longer need Input.GetAxis("Vertical") or horizontal because we used movementInput as our storage variable for
        // input actions associated with movement
        // instead of checking the input's horizontal and vertical axis components directly, we call the x and y components of the variables meant to store those values
        Vector2 move = new Vector2(movementInput.x, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        /*

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        */

        // Changes the height position of the player..
        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
