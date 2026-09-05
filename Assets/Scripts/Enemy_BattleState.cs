using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player;
    private float lastTimeWasInBattle;
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(player == null)
        {
            player = enemy.PlayerDetected().transform;
        }  

        if(ShouldRetreat())
        {
            rb.velocity = new Vector2(enemy.retreatVelocity.x * -DirectionToPlayer(),enemy.retreatVelocity.y);
            enemy.HandleFlip(DirectionToPlayer());
        }
    }

    

    public override void Update()
    {
        base.Update();

        if(enemy.PlayerDetected())
        {
            UpdateBattleTimer();       
        }
        if (BattleTimeIsOver())
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }   
        if (WithAttackRange())
        {
            stateMachine.ChangeState(enemy.attackState);
            return;
        }
        //若玩家处于检测范围但不处于攻击范围，移动敌人
        else
        {
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.velocity.y);
        }

    }

    private void UpdateBattleTimer() => lastTimeWasInBattle = Time.time;//更新战斗时间
    
    private bool BattleTimeIsOver() => Time.time > lastTimeWasInBattle + enemy.battleTimeDuration;//判断战斗是否结束

    private bool WithAttackRange() => DistanceToPlayer() < enemy.attackDistance;//攻击范围

    public bool ShouldRetreat() => DistanceToPlayer() < enemy.minRetreatDistance;//检测敌人是否需要后退
    

    private float DistanceToPlayer()
    {
        if(player == null)
        {
            return float.MaxValue;
        }

        return Mathf.Abs(player.position.x - enemy.transform.position.x);
    }

    private int DirectionToPlayer()
    {
        if(player == null) return 0;

        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }
}