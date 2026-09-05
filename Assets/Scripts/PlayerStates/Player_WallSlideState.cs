using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WallSlideState : PlayerState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Update()
    {
        base.Update();
        HandleWallSlide();

        if (input.Player.Jump.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.wallJumpState);
        } 
        if(player.wallDetected == false && !player.groundDetected)
        {
            player.Flip();
            stateMachine.ChangeState(player.fallState);
        }

        if (player.groundDetected)//地面检测
        {
            stateMachine.ChangeState(player.idleState);

            if(player.facingDir != player.moveInput.x)//如果角色朝向不等于输入的方向，则翻转
            {
                player.Flip();
            }
            
        }
    }

    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0 && player.groundDetected == false)//加速向下
        {
            player.SetVelocity(player.moveInput.x,rb.velocity.y);
        }
        else{
            player.SetVelocity(player.moveInput.x,rb.velocity.y*player.wallSlideSlowMultiplier);
        }
    }

  
}
