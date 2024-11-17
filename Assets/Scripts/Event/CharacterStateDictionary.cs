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
    /// Cpuキャラクターの種類選別メソッド
    /// </summary>
    /// <param name="cpuCharacters">すべてのCpuキャラクター</param>
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
    /// cpuキャラクターのステートマシンを初期化するメソッド
    /// </summary>
    /// <param name="cpuCharacter">cpu</param>
    /// <param name="allys">味方となるキャラ</param>
    /// <param name="enemies">敵となるキャラ</param>
    private void InitializeCpuStateMachine(ICpuCharacter cpuCharacter, List<BaseCharacter> allys, List<BaseCharacter> enemies)
    {
        List<CharacterStateView> otherAllys = new List<CharacterStateView>();
        List<CharacterStateView> otherenemys = new List<CharacterStateView>();

        otherAllys = ReturnStateView(cpuCharacter, allys);
        otherenemys = ReturnStateView(cpuCharacter, enemies);

        cpuCharacter.InitializeStateMachine(new OtherCharacterStatus(otherAllys, otherenemys));
    }

    /// <summary>
    /// 指定のcpuを省いたステートビューを返すメソッド
    /// </summary>
    /// <param name="cpuCharacter">省きたいキャラクター</param>
    /// <param name="characters">指定したすべてのキャラクター</param>
    /// <returns>省き済みのステートビュー</returns>
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
    /// 指定のサブクラスを持ったリストを返すメソッド
    /// </summary>
    /// <typeparam name="T">スーパークラスなど</typeparam>
    /// <param name="baseCharacters">探し出すリスト</param>
    /// <param name="subClassTypes">探したいサブクラス</param>
    /// <returns>指定のサブクラスを持ったスーパクラス</returns>
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
