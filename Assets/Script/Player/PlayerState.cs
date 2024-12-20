using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    
    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    private string animBoolName;

    protected float stateTimer;

    protected bool triggerCalled;
    

    public PlayerState(Player _player,PlayerStateMachine _stateMachine,string _animBoolName)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.Anim.SetBool(animBoolName, true);
        rb = player.Rb;
        
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        
        xInput = Input.GetAxisRaw("Horizontal");//raw为离散（—1，0，1），另一个是线性（-1~1）
        yInput = Input.GetAxisRaw("Vertical");
        player.Anim.SetFloat("yVelocity", rb.velocity.y);
    }
    
    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
    }

    public void FInishTrigger()
    {
        triggerCalled = true;
    }
}
