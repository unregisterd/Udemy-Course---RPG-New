using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;


    [Header("Battle 细节")]
    public float battleMoveSpeed = 3;
    public float attackDistance = 1.4f;
    public float battleTimeDuration = 5;
    public float minRetreatDistance = 1;
    public Vector2 retreatVelocity;


    [Header("移动信息")]
    public float idleTime = 2;
    public float moveSpeed = 1.4f;
    [Range(0,2)]
    public float moveAnimSpeedMultiplier = 1;

    [Header("玩家检测")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10;

    

    public RaycastHit2D PlayerDetected()
    {
        RaycastHit2D hit = 
                Physics2D.Raycast(playerCheck.position,Vector2.right * facingDir,playerCheckDistance,whatIsPlayer | whatIsGround);

        if(hit.collider == null  || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            return default;
        }

        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;//玩家距离检测
        Gizmos.DrawLine(playerCheck.position,new Vector3(playerCheck.position.x + (facingDir * playerCheckDistance),playerCheck.position.y));

        Gizmos.color = Color.blue;//攻击距离检测
        Gizmos.DrawLine(playerCheck.position,new Vector3(playerCheck.position.x + (facingDir * attackDistance),playerCheck.position.y));

        Gizmos.color = Color.green;//撤退
        Gizmos.DrawLine(playerCheck.position,new Vector3(playerCheck.position.x + (facingDir * minRetreatDistance),playerCheck.position.y));
    }
}
