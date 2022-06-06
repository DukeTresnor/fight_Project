using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    protected Player player;                                         // instance of the player object
    protected PlayerStateMachine stateMachine;                       // instance of the player state machine
    protected PlayerData playerData;                                 // instance of the player's data object   


    // Constructor
    public virtual void PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
    }

    // Contains methods that define the required behaviors
    public virtual void Enter()
    {

    }

    // No need for HandleInput(), since that will be handled in the InputHandler?

    public virtual void LogicUpdate()
    {
        // Handles core logic
    }

    public virtual void PhysicsUpdate()
    {
        // Handles physics logic and calculations
    }

    public virtual void Exit()
    {

    }

}
