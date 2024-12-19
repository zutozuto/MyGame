using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtk : PlayerState
{

    private int comboCounter;

    private float lastTimeAtk;
    private float comboWindow = 1;
    public PlayerAtk(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        if (comboCounter > 2 || Time.time >= lastTimeAtk + comboWindow)
        {
            comboCounter = 0;
        }
        player.Anim.SetInteger("comboCounter", comboCounter);

        stateTimer = .1f;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            rb.velocity = new Vector2(0, 0);
        }
        
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.IdleState);
        }

    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastTimeAtk = Time.time;
    }
}
