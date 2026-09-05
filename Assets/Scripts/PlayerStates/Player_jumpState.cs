using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_jumpState : Player_AirState
{
    public Player_jumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        //刚刚进入跳跃时，需要让物体上升，增加y的速度
        //player.SetVelocity(rb.velocity.x,player.jumpForce);
        player.SetVelocity(player.GetMovementInput().x,player.jumpForce);
    }

    public override void Update()
    {
        base.Update();
        //Debug.Log(player.moveInput);

        //如果y方向上的速度为负，物体下降，转换为下降状态
        //我们需要确保在转入下降状态时，我们不处于跳跃攻击状态，并且玩家不在地面上
        if(rb.velocity.y < 0 && stateMachine.currentState != player.jumpAttackState && !player.groundDetected)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }
}
