using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerAirState : PlayerState
{
    public PLayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected())
        {
            stateMachine.ChangeState(player.WallSlideState);
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (xInput !=0)
        {
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
