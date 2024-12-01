using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;

public class StateMachineGenerator: MonoBehaviour
{
    /// <summary>
    /// cpuで使うステートマシンを生成するメソッド
    /// </summary>
    /// <param name="characterDictionary">キャラクター辞典</param>
    public void InitializeStateMachine(CharacterDictionary characterDictionary, ObjectDictionary objectDictionary)
    {
        MyExtensionClass.CheckArgumentNull(characterDictionary, nameof(characterDictionary));

        //各Cpuにステートマシンを付与する
        foreach (ICpuCharacter cpuCharacter in characterDictionary.AllCpuCharacters)
        {
            //ステートマシンに使うデータの生成
            StateMachineInformation stateMachineInformation = new StateMachineInformation
                (
                 cpuCharacter,
                 cpuCharacter.CpuController,
                 GetStartNode(cpuCharacter, objectDictionary),
                 GetOtherCharacterStatus(cpuCharacter, characterDictionary)
                );

            Cpu.StateMachine stateMachine = new(stateMachineInformation);
            cpuCharacter.SetStateMachine(stateMachine);
            stateMachine.Initialize();
        }
    }

    /// <summary>
    /// Cpuキャラクターの種類選別メソッド
    /// </summary>
    /// <param name="cpuCharacters">すべてのCpuキャラクター</param>
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
    /// 指定のcpuを省いたステートビューを返すメソッド
    /// </summary>
    /// <param name="cpuCharacter">省きたいキャラクター</param>
    /// <param name="characters">指定したすべてのキャラクター</param>
    /// <returns>省き済みのステートビュー</returns>
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

    private StartStateNode GetStartNode(ICpuCharacter cpuCharacter, ObjectDictionary objectDictionary)
    {
        StartStateNode startStateNode = null;

        switch (cpuCharacter)
        {
            case BossCharacter:
                return startStateNode;

            case AllyCharacter allyCharacter:
                if (allyCharacter.TryGetComponent<HealerAction>(out HealerAction healer))
                {
                    return objectDictionary.GetHasComponent<HealStateMachine>()[0].StartStateNode;
                }
                else if (allyCharacter.TryGetComponent<TankAction>(out TankAction tank))
                {
                    return objectDictionary.GetHasComponent<TankStateMachine>()[0].StartStateNode;
                }
                break;
        }

        return startStateNode;
    }
}
