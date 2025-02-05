using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// �X�e�[�g�}�V���ɕK�v�ȏ���ێ�����N���X
/// </summary>
public class StateMachineInformation
{
    public ICpuCharacter CpuCharacterDate { get; private set; }
    public CpuController CpuControllerDate { get; private set; }
    public StartStateNode StartStateNodeDate { get; private set; }
    public OtherCharacterStatus OtherCharacterStatusDate { get; private set; }

    /// <summary>
    /// �R���X�g���N�^�Ŋe����������
    /// </summary>
    /// <param name="cpuCharacter">CPU�L�����N�^�[�̃f�[�^</param>
    /// <param name="cpuController">CPU�R���g���[���[</param>
    /// <param name="startNode">�J�n�X�e�[�g�m�[�h</param>
    /// <param name="otherCharacterStatus">���L�����N�^�[�̃X�e�[�^�X</param>
    public StateMachineInformation(ICpuCharacter cpuCharacter, CpuController cpuController, StartStateNode startNode, OtherCharacterStatus otherCharacterStatus)
    {
        CpuCharacterDate = cpuCharacter;
        CpuControllerDate = cpuController;
        StartStateNodeDate = startNode;
        OtherCharacterStatusDate = otherCharacterStatus;
    }
}

namespace Cpu
  {
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

        private StateMachineInformation _stateMachineInformation;

        /// <summary>
        /// ���݂̃X�e�[�g�m�[�h���擾
        /// </summary>
        public BaseStateNode CurrentStateNode { get; private set; }

        /// <summary>
        /// �X�e�[�g�}�V���̏���������
        /// </summary>
        public void Initialize()
        {
            // �X�e�[�g�m�[�h�̍X�V���s��
            UpdateStateNode();
            // �J�n�X�e�[�g�m�[�h�����݂̃X�e�[�g�ɐݒ�
            CurrentStateNode = _startStateNode;
            if (CurrentStateNode != null)
            {
                // �J�n�X�e�[�g�ɓ���
                CurrentStateNode.EnterState();
            }
        }

        /// <summary>
        /// ���̃X�e�[�g�ւ̑J�ڏ���
        /// </summary>
        /// <param name="nextStateNode">�J�ڐ�̃X�e�[�g�m�[�h</param>
        public void TransitionNextState(BaseStateNode nextStateNode)
        {
            BaseStateNode baseStateNode;

            // ���̃X�e�[�g���w�肳��Ă��Ȃ��ꍇ�A�x�[�X�X�e�[�g���X�g�̍ŏ����g�p
            if (nextStateNode == null)
            {
                baseStateNode = _baseStateNodes[0];
            }
            else
            {
                baseStateNode = nextStateNode;
            }

            // ���݂̃X�e�[�g����ޏo
            CurrentStateNode.ExitState();
            // �V�����X�e�[�g�ɐ؂�ւ�
            CurrentStateNode = baseStateNode;
            // �V�����X�e�[�g�ɓ���
            CurrentStateNode.EnterState();
        }

        /// <summary>
        /// �X�e�[�g�̍X�V���������s
        /// </summary>
        public void UpdateState()
        {
            if (CurrentStateNode != null)
            {
                // ���݂̃X�e�[�g�̍X�V�������Ăяo��
                CurrentStateNode.UpdateState();
            }
        }

        /// <summary>
        /// �X�e�[�g�}�V���̃R���X�g���N�^
        /// </summary>
        /// <param name="stateMachineInformation">�X�e�[�g�}�V���ɕK�v�ȃL�����N�^�[���</param>
        public StateMachine(StateMachineInformation stateMachineInformation)
        {
            // ������null�łȂ����Ƃ��m�F
            MyExtensionClass.CheckArgumentNull(stateMachineInformation, nameof(stateMachineInformation));

            // �e�t�B�[���h��������
            _cpuCharacter = stateMachineInformation.CpuCharacterDate;
            _cpuController = stateMachineInformation.CpuControllerDate;
            _startStateNode = stateMachineInformation.StartStateNodeDate;
            _otherCharacterStatus = stateMachineInformation.OtherCharacterStatusDate;

            if (_startStateNode != null)
            {
                // �J�n�X�e�[�g�m�[�h�ɃL�����N�^�[���ƃR���g���[���[��ݒ�
                _startStateNode.SetCharacterInformation(_cpuCharacter);
                _startStateNode.SetCpuContoller(_cpuController);
            }
        }

        /// <summary>
        /// �X�e�[�g�̃t���[���X�V����
        /// </summary>
        public void UpdateStateNode()
        {
            if (CurrentStateNode != null)
            {
                // ���݂̃X�e�[�g����ޏo
                CurrentStateNode.ExitState();
            }

            // �X�e�[�g�m�[�h�̃t���[�����Z�b�g����эX�V
            UpDateStateNodeFlow();

            // �e�x�[�X�X�e�[�g�m�[�h�ɃL�����N�^�[����ݒ�
            foreach (BaseStateNode baseState in _baseStateNodes)
            {
                baseState.SetCharacterInformation(_cpuCharacter);
                baseState.SetOtherCharacterInformation(_otherCharacterStatus);
                baseState.SetCpuContoller(_cpuController);
            }
        }

        /// <summary>
        /// �X�e�[�g�m�[�h�̃t���[�����Z�b�g���A�č\�z���郁�\�b�h
        /// </summary>
        private void UpDateStateNodeFlow()
        {
            if (_startStateNode != null)
            {
                // �x�[�X�X�e�[�g�m�[�h���X�g���N���A
                _baseStateNodes.Clear();
                // �J�n�X�e�[�g�m�[�h����o�̓m�[�h���擾
                _startStateNode.GetHasOutNode();
                OutNode[] outNodes = new OutNode[] { _startStateNode.OutNode };
                // �ċA�I�Ɏ��̃X�e�[�g���L���b�V��
                CacheNextState(outNodes);
            }
        }

        /// <summary>
        /// �X�e�[�g�̘A�����ċA�I�Ɋm�F���A�L���b�V���ɒǉ����郁�\�b�h
        /// </summary>
        /// <param name="outNodes">���݂̃X�e�[�g����̏o�̓m�[�h�z��</param>
        private void CacheNextState(OutNode[] outNodes)
        {
            // ������null�łȂ����Ƃ��m�F
            MyExtensionClass.CheckArgumentNull(outNodes, nameof(outNodes));

            foreach (OutNode outNode in outNodes)
            {
                // ���̃m�[�h���X�V
                outNode.UpdateNextNode();
                BaseStateNode nextStateNode = outNode.NextStateNode;

                // ���̃X�e�[�g�m�[�h���L���ŁA���[�v���Ă��Ȃ��ꍇ
                if (nextStateNode != null && !CheckStateLoop(_baseStateNodes, nextStateNode))
                {
                    // �x�[�X�X�e�[�g�m�[�h���X�g�ɒǉ�
                    _baseStateNodes.Add(nextStateNode);

                    // �ċA�I�Ɏ��̃X�e�[�g�m�[�h���L���b�V��
                    CacheNextState(nextStateNode.GetHasOutNode());
                }
            }
        }

        /// <summary>
        /// �X�e�[�g�̃��[�v���`�F�b�N���郁�\�b�h
        /// </summary>
        /// <param name="cacheBaseStates">�����̃L���b�V�����ꂽ�X�e�[�g�m�[�h���X�g</param>
        /// <param name="baseState">�ǉ��\��̃X�e�[�g�m�[�h</param>
        /// <returns>���[�v�����o���ꂽ�ꍇ��true�A����ȊO��false</returns>
        private bool CheckStateLoop(List<BaseStateNode> cacheBaseStates, BaseStateNode baseState)
        {
            foreach (BaseStateNode stateNode in cacheBaseStates)
            {
                if (stateNode == baseState)
                {
                    // ���[�v�����o���ꂽ�ꍇ
                    return true;
                }
            }

            return false;
        }
    }
}
