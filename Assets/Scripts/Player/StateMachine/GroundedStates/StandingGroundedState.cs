using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingGroundedState : GroundedState
{
    public StandingGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
    {

    }

    // Contains methods that define the required behaviors
    public override void Enter()
    {
        base.Enter();

        player.input.isGrounded = true;

        // Exit other animations and play idel animation
        Debug.Log("I'm Idle");

        /*
        Consider for here -- from Fuckaroundadillo ///
        player.SphereCol.material = playerData.uprightMaterial;

        player.Input.isGrounded = true;
        player.Input.UseGlideInput();

        player.Anim.SetBool("Jump", false);
        player.Anim.SetBool("Glide", false);
        player.Anim.SetBool("Hangtime", false);

        player.Anim.Play("Upright Idle");

        end of consideration ///
        */


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

        if (player.input.movementInput)
        {
            player.Walk();
            
            // Play animation
            //Debug.Log("Walking");
            //player.anim.SetBool("Walk", true);
        }
        else if (!player.input.movementInput)
        {
            // player should stop moving
            player.StopMoving();
            //Debug.Log("I stopped");

        }

        if (player.input.jumpInput)
        {
            player.Jump();
            // Play animation
            Debug.Log("Jumping");
            //player.anim.SetBool("Jump", true);
        }

        
    }

    public override void Exit()
    {
        base.Exit();
    }

}