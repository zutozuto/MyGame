
using System.Collections;
using UnityEngine;

public class Player : Entity
{

    public new GameObject gameObject;

    [Header("攻击")] 
    public Vector2[] atkMovement;

    public float counterAtkDuration = .2f;


    public bool isBusy { get; private set; }

    [Header("移动")]
    public float moveSpeed = 8f;
    public float jumpSpeed;
    
    [Header("冲刺")] 
    public float dashSpeed;
    public float dashTime;
    private float dashCd = .3f;
    private float dashTimer;
    public float dashDir { get;private set; }
    
    #region 状态

    

    public PlayerStateMachine StateMachine { get; private set; }
    
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PLayerAirState AirState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    
    public PlayerWallSlideState WallSlideState { get; private set; }
    
    public PlayerWallJump WallJumpState { get; private set; }
    
    public PlayerAtk AtkState { get; private set; }
    
    public PlayerCounterAtk CounterAtkState { get; private set; }
    
    #endregion

    protected override void Awake()
    {
        StateMachine = new PlayerStateMachine();
        
        IdleState = new PlayerIdleState(this, StateMachine,"Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState  = new PLayerAirState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, "Slide");
        WallJumpState = new PlayerWallJump(this, StateMachine, "Jump");
        AtkState = new PlayerAtk(this, StateMachine, "Atk");
        CounterAtkState = new PlayerCounterAtk(this, StateMachine, "CounterAtk");
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.currentState.Update();
        CheckDashInput();
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        
        isBusy = false;
    }

    public void AnimationTrigger() => StateMachine.currentState.FInishTrigger();

    private void CheckDashInput()
    {
        if (IsWallDetected() && !IsGroundDetected())
        {
           return; 
        }
        
        dashTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer < 0)
        {
            dashTimer = dashCd;
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
            {
                dashDir = facingDir;
            }

            StateMachine.ChangeState(DashState);
        }
    }
}
