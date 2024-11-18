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

        _allBosses = GetHasSubClass(baseCharacters, typeof(BossCharacter));
        _allAllys = GetHasSubClass(baseCharacters, typeof(PlayerCharacter));
        _allAllys.AddRange(GetHasSubClass(baseCharacters, typeof(AllyCharacter)));

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

    /// <summary>
    /// 指定のサブクラスを持ったリストを返すメソッド
    /// </summary>
    /// <typeparam name="T">スーパークラスなど</typeparam>
    /// <param name="baseCharacters">探し出すリスト</param>
    /// <param name="subClassTypes">探したいサブクラス</param>
    /// <returns>指定のサブクラスを持ったスーパクラス</returns>
    private List<T> GetHasSubClass<T>(IEnumerable<T> baseCharacters, Type subClassTypes)
    {
        MyExtensionClass.CheckArgumentNull(baseCharacters, nameof(baseCharacters));
        MyExtensionClass.CheckArgumentNull(subClassTypes, nameof(subClassTypes));

        List<T> cacheCharacters = new List<T>();

        foreach (T character in baseCharacters)
        {
            if (subClassTypes.IsAssignableFrom(character.GetType()))
            {
                cacheCharacters.Add(character);
            }
        }
        return cacheCharacters;
    }
}
