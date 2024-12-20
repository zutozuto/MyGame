using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    
    
    #region 组件
    public Animator Anim { get;private set; }
    public Rigidbody2D Rb { get;private set; }
    
    public EntityFx fx { get;private set; }
    #endregion

    [Header("击退")] 
    [SerializeField] private Vector2 knockDirection;

    private bool isKnock;
    
    [Header("碰撞")]
    public Transform atkCheck;
    public float atkCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    [Space]
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask groundLayer;
    
    public int facingDir { get; private set; } = 1;
    public bool facingRight = true;
    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFx>();
    }

    protected virtual void Update()
    {
        
    }

    public virtual void Damage()
    {
        fx.StartCoroutine("FlashFx");
        StartCoroutine("HitKnockBack");
    }

    protected virtual IEnumerator HitKnockBack()
    {
        isKnock = true;
        Rb.velocity = new Vector2(knockDirection.x * -facingDir, knockDirection.y);
        yield return new WaitForSeconds(0.07f);
        
        isKnock = false;
    }

    #region 碰撞
    public virtual bool IsGroundDetected()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
    }
    public virtual bool IsWallDetected()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, groundLayer);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position,new Vector3(groundCheck.position.x,groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position,new Vector3(wallCheck.position.x + wallCheckDistance,wallCheck.position.y));
        Gizmos.DrawWireSphere(atkCheck.position, atkCheckRadius);
    }
    
    #endregion
    
    #region 反转
    public  void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }

    public  void FlipController(float value)
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
    #endregion
    
    #region 速度
    public void ZeroVelocity()
    {
        if (isKnock)
            return;
        Rb.velocity = new Vector2(0, 0);
    }


    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnock)
            return;
        Rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion
}
