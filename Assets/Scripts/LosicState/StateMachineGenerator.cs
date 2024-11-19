using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineGenerator : MonoBehaviour
{
    public void InitializeStateMachine(ICpuCharacter[] cpuCharacters)
    {

    }

    /// <summary>
    /// cpuキャラクターのステートマシンを初期化するメソッド
    /// </summary>
    /// <param name="cpuCharacter">cpu</param>
    /// <param name="allys">味方となるキャラ</param>
    /// <param name="enemies">敵となるキャラ</param>
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
