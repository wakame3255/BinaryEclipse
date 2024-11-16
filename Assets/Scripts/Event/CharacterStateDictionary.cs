using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateDictionary : MonoBehaviour
{
    [SerializeField]
    private List<BaseCharacter> _allys = new List<BaseCharacter>();

    [SerializeField]
    private List<BaseCharacter> _enemys;

    [SerializeField]
    private List<ICpuCharacter> _cpuCharacters = new List<ICpuCharacter>();

    public void SetCharacterState(BaseCharacter[] baseCharacters)
    {
        _enemys = ReturnHasSubClass(baseCharacters, typeof(BossCharacter));
        _allys = ReturnHasSubClass(baseCharacters, typeof(PlayerCharacter));
        _allys.AddRange(ReturnHasSubClass(baseCharacters, typeof(AllyCharacter)));
    }

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
