using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : Entity
{
    public PlayerInputSet input{get;private set;}
    public  Player_idleState idleState{get;private set;}
    public  Player_moveState moveState{get;private set;}
    public Player_jumpState jumpState{get;private set;}
    public Player_fallState fallState{get;private set;}
    public Player_WallSlideState wallSlideState {get;private set;}
    public Player_WallJumpState wallJumpState{get;private set;}
    public Player_DashState dashState{get;private set;}
    public Player_BasicAttackState basicAttackState{get;private set;}
    public Player_JumpAttackState jumpAttackState{get;private set;}

    [Header("攻击信息")]
    public Vector2[] attackVelocity;
    public Vector2 jumpAttackVelocity;
    public float attackVelocityDuration = .1f;
    public float comboResetTime = 1;
    private Coroutine queuedAttackCo;
    

    [Header("移动信息")]
    public float moveSpeed;
    public float jumpForce=5;
    public Vector2 wallJumpForce;
    [Range(0,1)]
    public float inAirMoveMultyplier=.7f;
    [Range(0,1)]
    public float wallSlideSlowMultiplier=.7f;
    [Space]
    public float dashDuration = .25f;
    public float dashSpeed = 12;

    public Vector2 moveInput{get; private set;}

    protected override void Awake()
    {
        base.Awake();
        input = new PlayerInputSet();

        idleState = new Player_idleState(this,stateMachine,"idle");
        moveState = new Player_moveState(this,stateMachine,"move");
        jumpState = new Player_jumpState(this,stateMachine,"jumpFall");
        fallState = new Player_fallState(this,stateMachine,"jumpFall");
        wallSlideState = new Player_WallSlideState(this,stateMachine,"wallSlide");
        wallJumpState = new Player_WallJumpState(this,stateMachine,"jumpFall");
        dashState = new Player_DashState(this,stateMachine,"dash");
        basicAttackState = new Player_BasicAttackState(this,stateMachine,"basicAttack");
        jumpAttackState = new Player_JumpAttackState(this,stateMachine,"jumpAttack");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    private void OnEnable()
    {
        input.Enable();
        
        //input.Player.Movement.start  //按下按钮时立即触发
        //input.Player.Movement.performed //完全按下按钮一直执行
        //input.Player.Movement.canceled //松开按钮时停止
        input.Player.Movement.performed += ctx => moveInput=ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput=Vector2.zero;
    }
    private void OnDisable()
    {
        input.Disable();
    }

    public void EnterAttackStateWithDelay()
    {
        if(queuedAttackCo != null)
        {
            StopCoroutine(queuedAttackCo);  
        }
        queuedAttackCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }


    private IEnumerator EnterAttackStateWithDelayCo()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
    }

    //获取实时输入
    public Vector2 GetMovementInput()
    {
        return input.Player.Movement.ReadValue<Vector2>();
    }


}
