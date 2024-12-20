using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAtk : EnemyState
{
    private EnemySkeleton enemy;
    public SkeletonAtk(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        enemy.ZeroVelocity();

        if (triggerCalled)
        {
            stateMachine.ChangeMachine(enemy.BattleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAtk = Time.time;
    }
}
