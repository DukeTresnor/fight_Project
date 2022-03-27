using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference -- https://medium.com/nerd-for-tech/moving-with-the-new-input-system-unity-a6c9cb100808#:~:text=Click%20on%20Edit%20%3E%20Project%20Settings,both%20old%20and%20new%20system.


public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private PlayerInputActions _playerInputActions;
    private Rigidbody2D _rbody;
    private Vector2 _moveInput;


    void Awake()
    {
        // Initilize PlayerInputActions and the Rigidbody2D component for the player
        _playerInputActions = new PlayerInputActions();

        _rbody = GetComponent<Rigidbody2D>();
        if (_rbody is null)
        {
            Debug.Log("Rigidbody2D is NULL");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // FixedUpdate is called once per frame (?)
    void FixedUpdate()
    {
        // Read and store the Vector2 value from the Movement action
        _moveInput = _playerInputActions.Player_Map.Movement.ReadValue<Vector2>();
        // Setting Y axis to 0 (temporary fix)
        _moveInput.y = 0f;
        // Setting velocity of RigidBody2D -- input value * speed
        _rbody.velocity = _moveInput * _speed;
    }

    // ** OnAction Methods ** //

    // Enables the player's action map -- this contains the movement action (among other things eventually)
        // Should also include OnDisable() method
    private void OnEnable()
    {
        _playerInputActions.Player_Map.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Player_Map.Disable();
    }



    // ** End of OnAction Methods ** //


}
