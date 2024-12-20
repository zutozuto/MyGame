using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{

    //初始化
    public PlayerState currentState { get; private set; }

    public void Initialize(PlayerState _statState)
    {
        currentState = _statState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }

}
