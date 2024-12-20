
using UnityEngine;

public class EnemyState 
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;

    protected bool triggerCalled;/// <summary>
                                 /// 进入状态时为假
                                 /// 播完动画设为真
                                 /// 当为真则可以切换到别的状态
                                 /// </summary>
    protected float stateTimer;
    private string animBoolName;
    protected Rigidbody2D rb;

    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        enemyBase.Anim.SetBool(this.animBoolName, true);
        rb = enemyBase.Rb;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        enemyBase.Anim.SetBool(this.animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
