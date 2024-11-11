using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StateMachine
{
    [SerializeField]
    private List<BaseStateNode> _baseStateNodes = new List<BaseStateNode>();

    private CpuCharacter _cpuCharacter;
    private CpuController _cpuController;
    private StartStateNode _startStateNode;

    public BaseStateNode CurrentStateNode { get; private set; }

    public void Initialize(BaseStateNode baseState)
    {
        CurrentStateNode = baseState;
        CurrentStateNode.EnterState();
    }

    public void TransitionNextState(BaseStateNode nextStateNode)
    {
        BaseStateNode baseStateNode;
        if (nextStateNode == null)
        {
            baseStateNode = _baseStateNodes[0];
        }
        else
        {
            baseStateNode = nextStateNode;
        }

        CurrentStateNode.ExitState();
        CurrentStateNode = baseStateNode;
        CurrentStateNode.EnterState();
    }

    public void UpdateState()
    {
        if (CurrentStateNode != null)
        {
            CurrentStateNode.UpdateState();
        }
    }

    public StateMachine(CpuCharacter cpuCharacter, CpuController cpuController, StartStateNode startState)
    {
        _cpuCharacter = cpuCharacter;
        _cpuController = cpuController;
        _startStateNode = startState;

        _startStateNode.SetCharacterInfomation(cpuCharacter);
        _startStateNode.SetCpuContoller(cpuController);
    }

    public void UpdateStateNode()
    {
        if(CurrentStateNode != null)
        {
            CurrentStateNode.ExitState();
        }
            
        UpDateStateNodeFlow();

        foreach (BaseStateNode baseState in _baseStateNodes)
        {
            baseState.SetCharacterInfomation(_cpuCharacter);
            baseState.SetCpuContoller(_cpuController);
        }
    }

    private void UpDateStateNodeFlow()
    {
        if (_startStateNode != null)
        {
            _baseStateNodes.Clear();
            CheckNextState(_startStateNode.OutNode);
        }
    }

    /// <summary>
    /// ステートの連なりを再帰的に確認
    /// </summary>
    /// <param name="outNode"></param>
    private void CheckNextState(OutNode outNode)
    {
        if (outNode.NextNodeState != null)
        {
            _baseStateNodes.Add(outNode.NextNodeState);
            CheckNextState(outNode.NextNodeState.OutNode);
        }
    }
}
