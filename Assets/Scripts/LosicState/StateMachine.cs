using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StateMachine
{
    [SerializeField]
    private List<BaseStateNode> _baseStateNodes = new List<BaseStateNode>();

    private ICpuCharacter _cpuCharacter;
    private CpuController _cpuController;
    private StartStateNode _startStateNode;
    [SerializeField]
    private OtherCharacterStatus _otherCharacterStatus;

    public BaseStateNode CurrentStateNode { get; private set; }

    /// <summary>
    /// �X�e�[�g�̊J�n����
    /// </summary>
    /// <param name="baseState">�X�e�[�g</param>
    public void Initialize(BaseStateNode baseState)
    {
        CurrentStateNode = baseState;
        CurrentStateNode.EnterState();
    }

    /// <summary>
    /// �X�e�[�g�̐؂�ւ�����
    /// </summary>
    /// <param name="nextStateNode">�X�e�[�g</param>
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
    /// �X�e�[�g��Update����
    /// </summary>
    public void UpdateState()
    {
        if (CurrentStateNode != null)
        {
            CurrentStateNode.UpdateState();
        }
    }

    /// <summary>
    /// �X�e�[�g�}�V���̐���
    /// </summary>
    /// <param name="cpuCharacter">�L�����N�^�[���</param>
    /// <param name="cpuController">�R���g���[���[���</param>
    /// <param name="startState">�����̃X�e�[�g</param>
    public StateMachine(ICpuCharacter cpuCharacter, CpuController cpuController, StartStateNode startState, OtherCharacterStatus otherCharacter)
    {
        MyExtensionClass.CheckArgumentNull(cpuCharacter, nameof(cpuCharacter));
        MyExtensionClass.CheckArgumentNull(cpuCharacter, nameof(cpuController));
        MyExtensionClass.CheckArgumentNull(startState, nameof(startState));
        MyExtensionClass.CheckArgumentNull(otherCharacter, nameof(otherCharacter));

        _cpuCharacter = cpuCharacter;
        _cpuController = cpuController;
        _startStateNode = startState;
        _otherCharacterStatus = otherCharacter;

        _startStateNode.SetCharacterInformation(cpuCharacter);
        _startStateNode.SetCpuContoller(cpuController);
    }

    /// <summary>
    /// �X�e�[�g�̗���X�V
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
            baseState.SetOtherCharacterInformation(_otherCharacterStatus);
            baseState.SetCpuContoller(_cpuController);    
        }
    }

    /// <summary>
    /// �X�e�[�g���Z�b�g�ƍX�V���\�b�h
    /// </summary>
    private void UpDateStateNodeFlow()
    {
        if (_startStateNode != null)
        {
            _baseStateNodes.Clear();
            _startStateNode.GetHasOutNode();
            OutNode[] outNodes = new OutNode[] { _startStateNode.OutNode };
            CacheNextState(outNodes);
        }
    }

    /// <summary>
    /// �X�e�[�g�̘A�Ȃ���ċA�I�Ɋm�F
    /// </summary>
    /// <param name="outNode"></param>
    private void CacheNextState(OutNode[] outNodes)
    {
        MyExtensionClass.CheckArgumentNull(outNodes, nameof(outNodes));

        foreach (OutNode outNode in outNodes)
        {
            BaseStateNode nextStateNode = outNode.NextStateNode;

            if (nextStateNode != null)
            {
                _baseStateNodes.Add(nextStateNode);
               
                CacheNextState(nextStateNode.GetHasOutNode());
            }
        }
    }
}
