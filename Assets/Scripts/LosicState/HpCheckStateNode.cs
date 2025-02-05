using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HP�`�F�b�N�p�̃X�e�[�g�m�[�h�N���X
/// </summary>
public class HpCheckStateNode : BaseStateNode
{
    [SerializeField]
    private OutNode _falseNode; // HP�`�F�b�N�����s�����ꍇ�̑J�ڐ�m�[�h

    [SerializeField]
    private int _hp; // �`�F�b�N�Ώۂ�HP�l

    private bool _existsCharacter = default; // �w�肳�ꂽHP�ȉ��̃L�����N�^�[�����݂��邩�ǂ����̃t���O

    /// <summary>
    /// �X�e�[�g�ɓ���ۂ̏���
    /// </summary>
    public override void EnterState()
    {
        // �����L�����N�^�[�����[�v����HP���w��l�ȉ������m�F
        foreach (CharacterStateView characterState in _otherCharacters.Allys)
        {
            if (characterState.Hp <= _hp)
            {
                _existsCharacter = true;
                break; // �����𖞂����L�����N�^�[�������������߃��[�v�𔲂���
            }
            else
            {
                _existsCharacter = false;
            }
        }
        base.EnterState(); // ���N���X��EnterState���Ăяo��
    }

    /// <summary>
    /// �X�e�[�g�̍X�V����
    /// </summary>
    public override void UpdateState()
    {
        if (_existsCharacter)
        {
            // �����𖞂����L�����N�^�[�����݂���ꍇ�A�ʏ�̑J�ڐ�ɑJ��
            _cpuCharacter.StateMachine.TransitionNextState(_outNode.NextStateNode);
        }
        else
        {
            // �����𖞂����L�����N�^�[�����݂��Ȃ��ꍇ�A�ʂ̑J�ڐ�ɑJ��
            _cpuCharacter.StateMachine.TransitionNextState(_falseNode.NextStateNode);
        }
    }

    /// <summary>
    /// �X�e�[�g����ޏo����ۂ̏���
    /// </summary>
    public override void ExitState()
    {
        base.ExitState(); // ���N���X��ExitState���Ăяo��
    }

    /// <summary>
    /// ���̃X�e�[�g�m�[�h�����o�̓m�[�h���擾
    /// </summary>
    /// <returns>�o�̓m�[�h�̔z��</returns>
    public override OutNode[] GetHasOutNode()
    {
        _outNode.UpdateNextNode(); // �ʏ�̏o�̓m�[�h���X�V
        _falseNode.UpdateNextNode(); // ���s���̏o�̓m�[�h���X�V

        return new OutNode[] { _outNode, _falseNode }; // �o�̓m�[�h��z��ŕԂ�
    }
}
