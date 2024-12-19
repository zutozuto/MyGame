using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    
    [Header("移动")]
    public float moveSpeed = 8f;
    public float jumpSpeed;
    
    [Header("冲刺")] 
    public float dashSpeed;
    public float dashTime;
    private float dashCd = .3f;
    private float dashTimer;
    public float dashDir { get;private set; }

    [Header("碰撞")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [Space]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask groundLayer;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    #region 组件
    public Animator Anim { get;private set; }
    public Rigidbody2D Rb { get;private set; }
    #endregion


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
    
    #endregion

    private void Awake()
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
    }

    private void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.currentState.Update();
        CheckDashInput();
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

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        Rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public bool IsGroundDetected()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
    }
    public bool IsWallDetected()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position,new Vector3(groundCheck.position.x,groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position,new Vector3(wallCheck.position.x + wallCheckDistance,wallCheck.position.y));
    }

    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }

    public void FlipController(float value)
    {
        if (value > 0 && !facingRight)
        {
            Flip();
        }
        else if (value < 0 && facingRight)
        {
            Flip();
        }
    }
}
