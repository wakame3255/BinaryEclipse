using System;
using System.Collections.Generic;
using UnityEngine;
public class OtherCharacterStatus
{
    public OtherCharacterStatus(List<CharacterStateView> allys, List<CharacterStateView> enemys)
    {
        _allys = allys;
        _enemys = enemys;
    }

    private List<CharacterStateView> _allys = new List<CharacterStateView>();
    private List<CharacterStateView> _enemys = new List<CharacterStateView>();

    public List<CharacterStateView> Allys { get => _allys; }
    public List<CharacterStateView> Enemys { get => _enemys; }
}

public class CharacterStateDictionary : MonoBehaviour
{
    [SerializeField]
    private List<BaseCharacter> _allAllys = new List<BaseCharacter>();

    [SerializeField]
    private List<BaseCharacter> _allBosses;

    [SerializeField]
    private List<ICpuCharacter> _allCpuCharacters = new List<ICpuCharacter>();

    public void SetCharacterState(ObjectDictionary objectDictionary)
    {
        List<BaseCharacter> baseCharacters = objectDictionary.RetrunHasComponent<BaseCharacter>();
        
        _allBosses = ReturnHasSubClass(baseCharacters, typeof(BossCharacter));
        _allAllys = ReturnHasSubClass(baseCharacters, typeof(PlayerCharacter));
        _allAllys.AddRange(ReturnHasSubClass(baseCharacters, typeof(AllyCharacter)));

        _allCpuCharacters = objectDictionary.RetrunHasComponent<ICpuCharacter>();
        SelectionCpuCharacter(_allCpuCharacters);
    }

    /// <summary>
    /// Cpu�L�����N�^�[�̎�ޑI�ʃ��\�b�h
    /// </summary>
    /// <param name="cpuCharacters">���ׂĂ�Cpu�L�����N�^�[</param>
    private void SelectionCpuCharacter(List<ICpuCharacter> cpuCharacters)
    {
        foreach (ICpuCharacter cpuCharacter in cpuCharacters)
        {
            switch(cpuCharacter)
            {
                case BossCharacter bossCharacter:
                    InitializeCpuStateMachine(cpuCharacter, _allBosses, _allAllys);
                    break;

                case AllyCharacter allyCharacter:
                    InitializeCpuStateMachine(cpuCharacter, _allAllys, _allBosses);
                    break;
            }
        }
    }

    /// <summary>
    /// cpu�L�����N�^�[�̃X�e�[�g�}�V�������������郁�\�b�h
    /// </summary>
    /// <param name="cpuCharacter">cpu</param>
    /// <param name="allys">�����ƂȂ�L����</param>
    /// <param name="enemies">�G�ƂȂ�L����</param>
    private void InitializeCpuStateMachine(ICpuCharacter cpuCharacter, List<BaseCharacter> allys, List<BaseCharacter> enemies)
    {
        List<CharacterStateView> otherAllys = new List<CharacterStateView>();
        List<CharacterStateView> otherenemys = new List<CharacterStateView>();

        otherAllys = ReturnStateView(cpuCharacter, allys);
        otherenemys = ReturnStateView(cpuCharacter, enemies);

        cpuCharacter.InitializeStateMachine(new OtherCharacterStatus(otherAllys, otherenemys));
    }

    /// <summary>
    /// �w���cpu���Ȃ����X�e�[�g�r���[��Ԃ����\�b�h
    /// </summary>
    /// <param name="cpuCharacter">�Ȃ������L�����N�^�[</param>
    /// <param name="characters">�w�肵�����ׂẴL�����N�^�[</param>
    /// <returns>�Ȃ��ς݂̃X�e�[�g�r���[</returns>
    private List<CharacterStateView> ReturnStateView(ICpuCharacter cpuCharacter, List<BaseCharacter> characters)
    {
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

    /// <summary>
    /// �w��̃T�u�N���X�����������X�g��Ԃ����\�b�h
    /// </summary>
    /// <typeparam name="T">�X�[�p�[�N���X�Ȃ�</typeparam>
    /// <param name="baseCharacters">�T���o�����X�g</param>
    /// <param name="subClassTypes">�T�������T�u�N���X</param>
    /// <returns>�w��̃T�u�N���X���������X�[�p�N���X</returns>
    private List<T> ReturnHasSubClass<T>(IEnumerable<T> baseCharacters, Type subClassTypes)
    {
        List<T> cacheCharacters = new List<T>();

        foreach (T character in baseCharacters)
        {
            if (character.GetType() == subClassTypes)
            {
                cacheCharacters.Add(character);
            }
        }
        return cacheCharacters;
    }
}
