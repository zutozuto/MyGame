using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    [Header("击晕")] 
    public float stunDuration;
    public Vector2 stunPosition;
    
    [Header("移动")]
    public float moveSpeed;
    public float idleTime; //静止时间
    public float battleTime;

    [Header("攻击")] 
    public float atkDistance;

    public float atkCd;
    [HideInInspector]public float lastTimeAtk;
    
    [SerializeField]protected LayerMask whatIsPlayer;
    
    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir,50, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + atkDistance * facingDir, transform.position.y));
        
    }
}
