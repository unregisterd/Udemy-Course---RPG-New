using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Entity_AnimationTriggers : MonoBehaviour
{
    private Entity entity;
    private Entity_Combat entityCombat;
    void Awake()
    {
        entity = GetComponentInParent<Entity>();
        entityCombat = GetComponentInParent<Entity_Combat>();
    }
    private void CurrentStateTrigger()
    {
        //Debug.Log("Attack was over!");根据调试信息的不同，需要访问玩家，并让当前玩家知道退出状态的时间

        entity.CurrentStateAnimationTrigger();

    }
    private void AttackTrigger()//攻击检测
    {
        entityCombat.PerformAttack();
    }
    
}
