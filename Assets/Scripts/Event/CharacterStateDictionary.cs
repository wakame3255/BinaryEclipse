using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateDictionary : MonoBehaviour
{
    [SerializeField]
    private List<BaseCharacter> _allys = new List<BaseCharacter>();

    [SerializeField]
    private List<BaseCharacter> _enemys;

    public void SetCharacterState(BaseCharacter[] baseCharacters)
    {
        _enemys = ReturnHasSubClass(baseCharacters, typeof(BossCharacter));
        _allys = ReturnHasSubClass(baseCharacters, typeof(PlayerCharacter));
        _allys.AddRange(ReturnHasSubClass(baseCharacters, typeof(AllyCharacter)));
    }

    public void ReturnCharacterCondition()
    {
        
    }

    private List<T> ReturnHasSubClass<T>(T[] baseCharacters, Type types)
    {
        List<T> cacheCharacters = new List<T>();

        foreach (T character in baseCharacters)
        {
            if (character.GetType() == types)
            {
                cacheCharacters.Add(character);
            }
        }
        return cacheCharacters;
    }
}
