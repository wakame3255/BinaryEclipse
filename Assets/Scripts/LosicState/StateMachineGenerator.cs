using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;

public class StateMachineGenerator: MonoBehaviour
{
    [SerializeField, Required]
    StartStateNode _stateNode;
    /// <summary>
    /// cpu�Ŏg���X�e�[�g�}�V���𐶐����郁�\�b�h
    /// </summary>
    /// <param name="characterDictionary">�L�����N�^�[���T</param>
    public void InitializeStateMachine(CharacterDictionary characterDictionary)
    {
        MyExtensionClass.CheckArgumentNull(characterDictionary, nameof(characterDictionary));

        //�eCpu�ɃX�e�[�g�}�V����t�^����
        foreach (ICpuCharacter cpuCharacter in characterDictionary.AllCpuCharacters)
        {
            //�X�e�[�g�}�V�[���̃e���v������
            StartStateNode startState = Instantiate(_stateNode);
            //�X�e�[�g�}�V���Ɏg���f�[�^�̐���
            StateMachineInformation stateMachineInformation = new StateMachineInformation
                (
                 cpuCharacter,
                 cpuCharacter.CpuController,
                 startState,
                 GetOtherCharacterStatus(cpuCharacter, characterDictionary)
                );

            Cpu.StateMachine stateMachine = new(stateMachineInformation);
            cpuCharacter.SetStateMachine(stateMachine);
        }
    }

    /// <summary>
    /// Cpu�L�����N�^�[�̎�ޑI�ʃ��\�b�h
    /// </summary>
    /// <param name="cpuCharacters">���ׂĂ�Cpu�L�����N�^�[</param>
    private OtherCharacterStatus GetOtherCharacterStatus(ICpuCharacter cpuCharacters, CharacterDictionary characterState)
    {
        MyExtensionClass.CheckArgumentNull(cpuCharacters, nameof(cpuCharacters));
        MyExtensionClass.CheckArgumentNull(characterState, nameof(characterState));
        List<CharacterStateView> otherAllys = null;
        List<CharacterStateView> otherEnemys = null; ;

        switch (cpuCharacters)
        {
            case BossCharacter:
                otherAllys = GetStateView(cpuCharacters, characterState.AllBosses);
                otherEnemys = GetStateView(cpuCharacters, characterState.AllAllys);
                break;

            case AllyCharacter:
                otherAllys = GetStateView(cpuCharacters, characterState.AllAllys);
                otherEnemys = GetStateView(cpuCharacters, characterState.AllBosses);
                break;
        }

        return new OtherCharacterStatus(otherAllys, otherEnemys);
    }

    /// <summary>
    /// �w���cpu���Ȃ����X�e�[�g�r���[��Ԃ����\�b�h
    /// </summary>
    /// <param name="cpuCharacter">�Ȃ������L�����N�^�[</param>
    /// <param name="characters">�w�肵�����ׂẴL�����N�^�[</param>
    /// <returns>�Ȃ��ς݂̃X�e�[�g�r���[</returns>
    private List<CharacterStateView> GetStateView(ICpuCharacter cpuCharacter, List<BaseCharacter> characters)
    {
        MyExtensionClass.CheckArgumentNull(cpuCharacter, nameof(cpuCharacter));
        MyExtensionClass.CheckArgumentNull(characters, nameof(characters));

        List<CharacterStateView> characterStateViews = new List<CharacterStateView>();

        foreach (BaseCharacter character in characters)
        {
            if (character != (BaseCharacter)cpuCharacter)
            {
                characterStateViews.Add(character.CharacterStatusView);
            }
        }

        return characterStateViews;
    }
}
