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

    /// <summary>
    /// ステートの開始処理
    /// </summary>
    /// <param name="baseState">ステート</param>
    public void Initialize(BaseStateNode baseState)
    {
        CurrentStateNode = baseState;
        CurrentStateNode.EnterState();
    }

    /// <summary>
    /// ステートの切り替え処理
    /// </summary>
    /// <param name="nextStateNode">ステート</param>
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

    /// <summary>
    /// ステートのUpdate処理
    /// </summary>
    public void UpdateState()
    {
        if (CurrentStateNode != null)
        {
            CurrentStateNode.UpdateState();
        }
    }

    /// <summary>
    /// ステートマシンの生成
    /// </summary>
    /// <param name="cpuCharacter">キャラクター情報</param>
    /// <param name="cpuController">コントローラー情報</param>
    /// <param name="startState">初期のステート</param>
    public StateMachine(CpuCharacter cpuCharacter, CpuController cpuController, StartStateNode startState)
    {
        _cpuCharacter = cpuCharacter;
        _cpuController = cpuController;
        _startStateNode = startState;

        _startStateNode.SetCharacterInformation(cpuCharacter);
        _startStateNode.SetCpuContoller(cpuController);
    }

    /// <summary>
    /// ステートの流れ更新
    /// </summary>
    public void UpdateStateNode()
    {
        if(CurrentStateNode != null)
        {
            CurrentStateNode.ExitState();
        }
            
        UpDateStateNodeFlow();

        foreach (BaseStateNode baseState in _baseStateNodes)
        {
            baseState.SetCharacterInformation(_cpuCharacter);
            baseState.SetCpuContoller(_cpuController);
        }
    }

    /// <summary>
    /// ステートリセットと更新メソッド
    /// </summary>
    private void UpDateStateNodeFlow()
    {
        if (_startStateNode != null)
        {
            _baseStateNodes.Clear();
            _startStateNode.OutNode.UpdateNextNode();
            CheckNextState(_startStateNode.OutNode);
        }
    }

    /// <summary>
    /// ステートの連なりを再帰的に確認
    /// </summary>
    /// <param name="outNode"></param>
    private void CheckNextState(OutNode outNode)
    {
        BaseStateNode nextStateNode = outNode.NextStateNode;

        if (nextStateNode != null)
        {
            _baseStateNodes.Add(nextStateNode);
            nextStateNode.OutNode.UpdateNextNode();
            CheckNextState(nextStateNode.OutNode);
        }
    }
}
