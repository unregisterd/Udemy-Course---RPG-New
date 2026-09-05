using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine//状态机：负责转换状态
{
    public EntityState currentState{get;private set;}
    protected string stateName;

    public void  Initialize(EntityState startState)//初始化状态
    {
        currentState = startState;
        currentState.Enter();
    }
    public void ChangeState(EntityState newState)//改变状态
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void UpdateActiveState()//更新激活状态
    {
        currentState.Update();
    }
    
}

