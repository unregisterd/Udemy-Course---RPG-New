using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_fallState : Player_AirState
{
    public Player_fallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        //检测物体是否探测到了下方的地面，if yes...go to idle state
        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (player.wallDetected)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
