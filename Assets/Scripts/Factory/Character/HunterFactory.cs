using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterFactory : BaseCharacterFactory
{
    public HunterFactory(List<CharacterInformation> hunterCharacter)
    {
        _characterInformation = hunterCharacter;
    }

    public override void GenerateCharacter()
    {
        foreach (CharacterInformation hunter in _characterInformation)
        {
            Instantiate(hunter.CharacterPrefab);
        }
    }
    
}
