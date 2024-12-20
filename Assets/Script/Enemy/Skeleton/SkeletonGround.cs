using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGround : EnemyState
{
    protected EnemySkeleton enemy;

    protected Transform player;
    public SkeletonGround(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;
    }

    public override void Update()
    {
        base.Update();
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < 2f)
        {
            stateMachine.ChangeMachine(enemy.BattleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
