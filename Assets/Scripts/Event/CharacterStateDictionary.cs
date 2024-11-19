using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OtherCharacterStatus
{
    public OtherCharacterStatus(List<CharacterStateView> allys, List<CharacterStateView> enemys)
    {
        _allys = allys;
        _enemys = enemys;
    }

    [SerializeField]
    private List<CharacterStateView> _allys = new List<CharacterStateView>();
    [SerializeField]
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
        MyExtensionClass.CheckArgumentNull(objectDictionary, nameof(objectDictionary));

        List<BaseCharacter> baseCharacters = objectDictionary.GetHasComponent<BaseCharacter>();

        _allBosses = objectDictionary.GetHasSubClass(baseCharacters, typeof(BossCharacter));
        _allAllys = objectDictionary.GetHasSubClass(baseCharacters, typeof(PlayerCharacter));
        _allAllys.AddRange(objectDictionary.GetHasSubClass(baseCharacters, typeof(AllyCharacter)));

        _allCpuCharacters = objectDictionary.GetHasComponent<ICpuCharacter>();
        SelectionCpuCharacter(_allCpuCharacters);
    }

    /// <summary>
    /// Cpuキャラクターの種類選別メソッド
    /// </summary>
    /// <param name="cpuCharacters">すべてのCpuキャラクター</param>
    private void SelectionCpuCharacter(List<ICpuCharacter> cpuCharacters)
    {
        MyExtensionClass.CheckArgumentNull(cpuCharacters, nameof(cpuCharacters));

        foreach (ICpuCharacter cpuCharacter in cpuCharacters)
        {
            switch (cpuCharacter)
            {
                case BossCharacter:
                    InitializeCpuStateMachine(cpuCharacter, _allBosses, _allAllys);
                    break;

                case AllyCharacter:
                    InitializeCpuStateMachine(cpuCharacter, _allAllys, _allBosses);
                    break;
            }
        }
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

    
}
