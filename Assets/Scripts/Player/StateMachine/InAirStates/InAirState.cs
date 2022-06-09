using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : PlayerState
{
    public InAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

}