using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SkeletonStunned :  EnemyState
{
    private EnemySkeleton enemy;
    public SkeletonStunned(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.fx.InvokeRepeating("RedBlink",0,.1f);
        stateTimer = enemy.stunDuration;
        
        rb.velocity = new Vector2(-enemy.facingDir * enemy.stunPosition.x,enemy.stunPosition.y); 
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.ChangeMachine(enemy.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("CancelRedBlink",0);
    }
}
