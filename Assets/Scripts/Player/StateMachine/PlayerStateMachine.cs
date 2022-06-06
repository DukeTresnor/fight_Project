using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    // Neds to store the current state as a var
    // handles entering and exiting a state -- contains initilize and change state methods

    public PlayerState CurrentState { get; private set; }

    // Starts up the state machine with parameter startingState
    public void Initilize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    // Handles transitions
    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
