using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemySkeleton : Enemy
{

    #region 状态

    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get;private set; }
    
    public SkeletonBattle BattleState { get; private set; }
    
    public SkeletonAtk AtkState { get; private set; }
    
    public SkeletonStunned StunnedState { get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        BattleState = new SkeletonBattle(this, stateMachine, "Move", this);
        AtkState = new SkeletonAtk(this, stateMachine, "Atk", this);
        StunnedState = new SkeletonStunned(this, stateMachine, "Stunned", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.U))
        {
            stateMachine.ChangeMachine(StunnedState);
        }
        
    }
}
