using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Reference -- https://medium.com/nerd-for-tech/moving-with-the-new-input-system-unity-a6c9cb100808#:~:text=Click%20on%20Edit%20%3E%20Project%20Settings,both%20old%20and%20new%20system.
// Jumping reference -- https://medium.com/nerd-for-tech/implementing-a-jump-mechanic-unity-6420b106e47a

public class Player : MonoBehaviour
{
    // Creates a version of a header file?
    [Header("Ground Check Properties")]
    
    [SerializeField]
    private float _groundCheckHeight;                        // Height of the box that's below the player -- used in seeing if the plauer is on the ground with OverlapBox()
    
    [SerializeField]
    private LayerMask _groundMask;                           // OverlapBox() -- Used to check for colliders within the level layer (ground layer)
    
    [SerializeField]
    private float _disableGCTime;                            // How much time the level (ground) check is disabled for when jumping -- helps avoid resetting the jumping bool value

    [SerializeField]
    private float _speed;                                    // Scalar used for rate of player movement
    
    [SerializeField]
    private float _jumpPower;                                // Initial jump velocity
    

    // This might need to change
    [Range(1f, 5f)]
    public float _jumpFallGravityMultiplier;                // Gravity scale applied after reaching apex of jump.


    private PlayerInputActions _playerInputActions;          // Holds reference to the input action asset that contains player actions -- like moving or jumping.
                                                             //   Do I need multiples of these to enable a 2nd player?
    private Rigidbody2D _rbody;                              // Holds the reference to the RigidBody2D of the player
    private BoxCollider2D _collider;                         // Holds a reference to the BoxCollider2D of the player
    private Vector2 _moveInput;
    
    private Vector2 _boxCenter;                              // Gives the central coordinate of the box that is checking to see if the player is on the level (ground)
    private Vector2 _boxSize;                                // Gives the size (width, height) of the box that is checking to see if the player is on the level (ground)
    private bool _jumping;                                   // Boolean that checks if the player is jumping or not (stores "player's jumping" state)
    private float _initialGravityScale;                      // Stores the initial gravity scale value of the rigid body
    private bool _groundCheckEnabled = true;                 // Indicates if the level (ground) check is enabled or not
    private WaitForSeconds _wait;                            // Waits within a coroutine until renabling the level (ground) check
                                                             //   Might need to change this and refernces to it to enable state machine for rollback netcode implementation



    void Awake()
    {
        // Initilize PlayerInputActions and the Rigidbody2D component for the player
        _playerInputActions = new PlayerInputActions();

        _rbody = GetComponent<Rigidbody2D>();
        if (_rbody is null)
        {
            Debug.Log("Rigidbody2D is NULL");
        }

        _initialGravityScale = _rbody.gravityScale;

        _collider = GetComponent<BoxCollider2D>();
        if (_collider is null)
        {
            Debug.Log("BoxCollider2D is NULL");
        }

        _wait = new WaitForSeconds(_disableGCTime);

        // Tutorial doesn't have Jump_performed method call with parenthesis?
        _playerInputActions.Player_Map.Jump.performed += Jump_performed;


    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // FixedUpdate is called once per frame (?)
    void FixedUpdate()
    {

        // Eventually modify using a HandleMovement() method?
        // Read and store the Vector2 value from the Movement action
        _moveInput = _playerInputActions.Player_Map.Movement.ReadValue<Vector2>();
        // Setting Y axis to 0 (temporary fix)
        //_moveInput.y = 0f;
        // Setting velocity of RigidBody2D -- input value * speed
        _rbody.velocity = _moveInput * _speed;

        // Handles gravity and jumping conditions
        HandleGravity();


    }








    // ** OnAction Methods ** //

    // Enables the player's action map -- this contains the movement action (among other things eventually) -- good practice to have these
        // Should also include OnDisable() method
    private void OnEnable()
    {
        _playerInputActions.Player_Map.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Player_Map.Disable();
    }


    private void OnDrawGizmos()
    {
        if(_jumping)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireCube(_boxCenter, _boxSize);
    }

    // ** End of OnAction Methods ** //




    // ** Other Methods ** //


    // This is called each time the player performs the Jump action. Shouldn't this be called "OnJump"?
    //   No -- this is the jump comamnd function -- it is more similar to Jump()
    private void Jump_performed(InputAction.CallbackContext context)
    {
        // Check if the player is grounded
        if (IsGrounded())
        {
            // Adds velocity to the y axis, according to the value of _jumpPower;
            _rbody.velocity += Vector2.up * _jumpPower;
            _jumping = true;
            StartCoroutine(EnableGroundCheckAfterJump());
            Debug.Log("Jump performed");
        }
    }

    // Returns true if the Player is on the ground and false otherwise
    //   Look to this method to implement hitbox collision scripts for moves?
    private bool IsGrounded()
    {


        // Calculate central coordinate of the level (ground) as the central coordinate of the collider, plus half the height of the collider, plus the half of the
        //   height from the level (ground). ??
        _boxCenter = new Vector2(_collider.bounds.center.x, _collider.bounds.center.y)
                    + (Vector2.down * (_collider.bounds.extents.y + (_groundCheckHeight / 2f)));
        
        // Setting the size of the box (width, height) -- witdh is same as the collider, height is based on desired height (?)
        _boxSize = new Vector2(_collider.bounds.size.x, _groundCheckHeight);

        // Using OverLapBox method ...
        var groundBox = Physics2D.OverlapBox(_boxCenter, _boxSize, 0f, _groundMask);

        // If the collider returned isn't null, the player is on the ground -- ie there is a collision that exists (?). Otherwise, there isn't a collision
        //   and so the player is not on the ground.
        if (groundBox != null)
        {
            return true;
        }

        return false;

    }

    // Determines when the player isn't jumping anymore, and changes the gravity scale when it's falling
    private void HandleGravity()
    {
        if(_groundCheckEnabled && IsGrounded())
        {
            _jumping = false;
            Debug.Log("I'm not jumping");
        }
        else if(_jumping && (_rbody.velocity.y < 0f) )        // Jump Fall  -- error here -- comparing rigidbody 2d and float
        {
            _rbody.gravityScale = _initialGravityScale * _jumpFallGravityMultiplier;
            Debug.Log("I'm falling after jumping!");
        }
        else                                                  // Normal Fall
        {
            _rbody.gravityScale = _initialGravityScale;
            Debug.Log("I'm just falling");
        }
    }

    // ** End of Other Methods ** //


    // ** Coroutines ** //


    // This coroutine changes the boolean value that indicates that the ground check should be enabled or disabled -- ie it handles jump check switching ?
    //   This is meant to disable the ground check for some amount of time after the Player has landed from a jump -- ie this is some sort of landing lag?
    //   Could be useful to use in some fashion later for delay on certain moves? Or, run to prevent jumping out of lag of certain moves...
    private IEnumerator EnableGroundCheckAfterJump()
    {
        _groundCheckEnabled = false;
        yield return _wait;
        _groundCheckEnabled = true;
    }

    // ** End of Coroutines ** //


}
