using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineGenerator : MonoBehaviour
{
    public void InitializeStateMachine(ICpuCharacter[] cpuCharacters)
    {

    }

    /// <summary>
    /// cpu�L�����N�^�[�̃X�e�[�g�}�V�������������郁�\�b�h
    /// </summary>
    /// <param name="cpuCharacter">cpu</param>
    /// <param name="allys">�����ƂȂ�L����</param>
    /// <param name="enemies">�G�ƂȂ�L����</param>
    private void InitializeCpuStateMachine(ICpuCharacter cpuCharacter, List<BaseCharacter> allys, List<BaseCharacter> enemies)
    {
        MyExtensionClass.CheckArgumentNull(cpuCharacter, nameof(cpuCharacter));
        MyExtensionClass.CheckArgumentNull(allys, nameof(allys));
        MyExtensionClass.CheckArgumentNull(enemies, nameof(enemies));

        List<CharacterStateView> otherAllys = GetStateView(cpuCharacter, allys);
        List<CharacterStateView> otherenemys = GetStateView(cpuCharacter, enemies);

        cpuCharacter.InitializeStateMachine(new OtherCharacterStatus(otherAllys, otherenemys));
    }
}
