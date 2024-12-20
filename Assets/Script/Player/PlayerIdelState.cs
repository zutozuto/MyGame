using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ZeroVelocity();
    }

    public override void Update()
    {
        base.Update();
        
        if(xInput == player.facingDir && player.IsWallDetected())
            return;
        if (xInput != 0 && !player.isBusy)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
