using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : PlayerState
{
    public GroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
    {

    }

    // Contains methods that define the required behaviors
    public override void Enter()
    {
        base.Enter();
    }

    // No need for HandleInput(), since that will be handled in the InputHandler?

    public override void LogicUpdate()
    {
        // Handles core logic
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        // Handles physics logic and calculations
        base.PhysicsUpdate();

        //if (player.input.jumpInput)
        //{
        //    player.Jump();
        //    // Play animation
        //    Debug.Log("Jumping");
        //    //player.anim.SetBool("Jump", true);
        //}

    }

    public override void Exit()
    {
        base.Exit();
    }

}