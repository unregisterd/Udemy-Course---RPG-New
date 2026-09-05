using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState
{
    
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Animator anim;
    protected Rigidbody2D rb;
    
    protected float stateTimer;
    protected bool triggerCalled;

    public EntityState(StateMachine stateMachine,string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()//进入状态
    {
        //每次状态发生变化时调用
        anim.SetBool(animBoolName,true);
        triggerCalled = false;
        
    }

    
    public virtual void Update()//更新状态
    {
        //运行状态逻辑
        //Debug.Log("I run update of "+animBoolName);
        stateTimer -= Time.deltaTime;
        UpdateAnimationParameters();
    }
    public void AnimationTrigger()
    {
        triggerCalled = true;
    }
    public virtual void Exit()//退出状态
    {
        //每次退出状态并切换到新状态时调用
        anim.SetBool(animBoolName,false);   
    }

    public virtual void UpdateAnimationParameters()
    {
        
    }
    
}
