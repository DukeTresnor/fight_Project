using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public StandingGroundedState StandingGroundState {get; private set;}
    public CrouchingGroundedState CrouchingGroundState {get; private set;}
    public InAirState AirState {get; private set;}


    [SerializeField]
    private PlayerData playerData;

    #endregion


    #region Movement Variables
    public Vector2 moveVec;

    #endregion


    #region Components
    public Animator anim;
    public InputHandler input {get; private set;}
    public Rigidbody2D rigidBody;
    // something for the box colliders? public BoxCollider boxCollider;

    #endregion



    #region Unity Callback Functions

    // Player instance vars get state -- should initilize each of your action states
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        StandingGroundState = new StandingGroundedState(this, StateMachine, playerData);
        CrouchingGroundState = new CrouchingGroundedState(this, StateMachine, playerData);
        AirState = new InAirState(this, StateMachine, playerData);
    }

    // Have input get input handler component, initialize state machine
    private void Start()
    {
        input = GetComponent<InputHandler>();           // Get components of the inputhandler
        playerData = GetComponent<PlayerData>();
        rigidBody = GetComponent<Rigidbody2D>();
        StateMachine.Initilize(StandingGroundState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    public void PrintCurrentState()
    {
        Debug.Log(StateMachine.CurrentState);
        //print(StateMachine.CurrentState);
        // Maybe use print(Statemachine.CurrentState) ?
    }


    #region Player Actions

    public void Walk()
    {
        moveVec = input.rawMoveInput;
        Vector3 inputVelocity = new Vector3(moveVec.x, 0, moveVec.y);
        Vector3 moveVelocity = inputVelocity * playerData.walkSpeed;
        rigidBody.AddForce(moveVelocity);
    }

    public void StopMoving()
    {
        rigidBody.velocity = Vector3.zero;
    }

    public void Crouch()
    {

    }

    public void Dash()
    {

    }

    public void Jump()
    {

        // come back after fleshing out input handler
        input.isJumping = true;

        Vector3 jumpVelocity = Vector3.up * playerData.jumpForce;

        rigidBody.AddForce(jumpVelocity);
    }

    #endregion




}
