using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattle : EnemyState
{
    private Transform player;
    private EnemySkeleton enemy;
    private int moveDir;
    public SkeletonBattle(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            if (enemy.IsPlayerDetected().distance < enemy.atkDistance && CanAtk())
            {
                stateMachine.ChangeMachine(enemy.AtkState);
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position,enemy.transform.position) > 10)
            {
                stateMachine.ChangeMachine(enemy.idleState);
            }
        }

        if (player.position.x > enemy.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.position.x < enemy.transform.position.x)
        {
            moveDir = -1;
        }
        
        enemy.SetVelocity(enemy.moveSpeed * moveDir,rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAtk()
    {
        if (Time.time >= enemy.lastTimeAtk + enemy.atkCd)
        {
            enemy.lastTimeAtk = Time.time;
            return true;
        }
        
        return false;
    }
}
