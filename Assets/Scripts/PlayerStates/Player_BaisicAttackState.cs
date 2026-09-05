using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    private float attackVelocityTimer;

    private float lastTimeAttacked;

    private const int FirstComboIndex = 1;//从一号开始组合索引，用于攻击动画
    private int comboIndex = 1;
    private int comboLimit = 3;
    private int attackDir;
    private bool comboAttackQueued;
    
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        comboLimit = player.attackVelocity.Length;
    }
    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;
        ResetComboIndexIfNeeded();
        //根据输入确定攻击方向
        attackDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDir;
        // if(player.moveInput.x != 0)
        // {
        //     attackDir = ((int)player.moveInput.x);
        // }
        // else
        // {
        //     attackDir = player.facingDir;
        // }

        anim.SetInteger("basicAttackIndex",comboIndex);
        ApplyAttackVelocity();
    }
    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if (input.Player.Attack.WasPressedThisFrame())
        {
            QueueNextAttack();
        }

        if (triggerCalled)
        {
           HandleStateExit();
        }
    }

    

    public override void Exit()
    {
        base.Exit();
        comboIndex++;
        lastTimeAttacked = Time.time;

        //记录攻击的时间
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)
            {
                anim.SetBool(animBoolName,false);
                player.EnterAttackStateWithDelay();
                //stateMachine.ChangeState(player.basicAttackState);
            }
            else
            {
                stateMachine.ChangeState(player.idleState);
            }
    }

    private void QueueNextAttack()
    {
        if(comboIndex < comboLimit)
        {
            comboAttackQueued = true;
        }
    }

    private void ResetComboIndexIfNeeded()//组合技
    {
        //检查间隔时间是否超过指定时间，如果超过，则重置combo index
        if(Time.time > lastTimeAttacked + player.comboResetTime || comboIndex > comboLimit)
        {
            comboIndex = FirstComboIndex;
        }
        if(comboIndex > comboLimit)
        {
            comboIndex = FirstComboIndex;
        }
        
    }
    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;
        if(attackVelocityTimer < 0)
        {
            player.SetVelocity(0,rb.velocity.y);
        }
        
        
    }
    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex-1];
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x * attackDir,attackVelocity.y);
    }


    
}
