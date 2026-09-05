using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim{get;private set;}
    public Rigidbody2D rb{get;private set;}
    protected StateMachine stateMachine;
    
    public bool facingRight=true;//默认向右----------------
    public int facingDir{get;private set;}=1;
    
    [Header("碰撞检测")]
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    public bool groundDetected{get;private set;}
    public bool wallDetected{get;private set;}
    

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb=GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        
    }

    protected virtual void Start()
    {
        
    }
    
    protected virtual void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }
    
    public void CurrentStateAnimationTrigger()
    {
        stateMachine.currentState.AnimationTrigger();
    }

    public void SetVelocity(float xVelocity,float yVelocity)
    {
        rb.velocity=new Vector2(xVelocity,yVelocity);
        HandleFlip(xVelocity);//处理翻转的问题
    }

    public void HandleFlip(float xVelocity)
    {
        //如果角色速度方向向右而面向左侧，翻转；如果角色速度方向向左而面向右侧，翻转
        if(xVelocity > 0 && facingRight == false)
        {
            Flip();
        }
        else if(xVelocity < 0 && facingRight == true)
        {
            Flip();
        }

    }

    public void Flip()
    {
        transform.Rotate(0,180,0);
        facingRight = !facingRight;
        facingDir = facingDir*(-1);
    }

    private void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position,Vector2.down,groundCheckDistance,whatIsGround);

        if(secondaryWallCheck != null)
        {
            wallDetected = Physics2D.Raycast(primaryWallCheck.position,Vector2.right*facingDir,wallCheckDistance,whatIsGround)
                        && Physics2D.Raycast(secondaryWallCheck.position,Vector2.right * facingDir,wallCheckDistance,whatIsGround);
        }
        else
        {
            wallDetected = Physics2D.Raycast(primaryWallCheck.position,Vector2.right*facingDir,wallCheckDistance,whatIsGround);
        }
    }
        

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position,groundCheck.position + new Vector3(0,-groundCheckDistance));
        //检测墙---1
        Gizmos.DrawLine(primaryWallCheck.position,primaryWallCheck.position + new Vector3(wallCheckDistance*facingDir,0));
        //检测墙---2
        if(secondaryWallCheck != null)
        {
            Gizmos.DrawLine(secondaryWallCheck.position,secondaryWallCheck.position + new Vector3(wallCheckDistance*facingDir,0));
        }
        
    }

    
}
