using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StateMachine
{
    [SerializeField]
    StartStateNode _startStateNode;

    [SerializeField]
    List<BaseStateNode> _baseStateNodes = new List<BaseStateNode>();

    public BaseStateNode CurrentStateNode { get; private set; }
    private CpuController _cpuController;

    public void Initialize(BaseStateNode baseState)
    {
        CurrentStateNode = baseState;
        CurrentStateNode.EnterState();
    }

    public void TransitionNextState(BaseStateNode nextStateNode)
    {
        CurrentStateNode.ExitState();
        CurrentStateNode = nextStateNode;
        CurrentStateNode.EnterState();
    }

    public void UpdateState()
    {
        if (CurrentStateNode != null)
        {
            CurrentStateNode.UpdateState();
        }
    }

    public StateMachine(BaseCharacter baseCharacter, CpuController cpuController)
    {
        foreach (BaseStateNode baseState in _baseStateNodes)
        {
            baseState.SetCharacterInfomation(baseCharacter);
            baseState.SetCpuContoller(cpuController);
        }
    }
}
