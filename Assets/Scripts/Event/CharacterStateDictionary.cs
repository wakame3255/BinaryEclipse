using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateDictionary : MonoBehaviour
{
    private List<BaseCharacter>_allys = new List<BaseCharacter>();
    private List<BaseCharacter> _enemys;

    public void SetCharacterState(BaseCharacter[] baseCharacters)
    {
        //_allys.Add(ReturnHasSubClass<PlayerCharacter>());
    }

    private List<BaseCharacter> ReturnHasSubClass(BaseCharacter[] baseCharacters, BaseCharacter baseCharacter)
    {
        List<BaseCharacter> cacheCharacters = new List<BaseCharacter>();

        foreach (BaseCharacter character in baseCharacters)
        {
            if(character is types)
            {
                cacheCharacters.Add(character);
            }
        }
        return cacheCharacters;
    }
}
