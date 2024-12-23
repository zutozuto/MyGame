using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAtk : PlayerState
{
    public PlayerCounterAtk(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.counterAtkDuration;
        player.Anim.SetBool("CounterAtkSuccess",false);
    }

    public override void Update()
    {
        base.Update();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.atkCheck.position, player.atkCheckRadius);

        foreach (var hit  in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanStun())
                {
                    stateTimer = 10;
                    player.Anim.SetBool("CounterAtkSuccess",true);
                }
            }
        }

        if (stateTimer < 0 || triggerCalled)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
