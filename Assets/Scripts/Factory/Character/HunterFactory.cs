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
        for (int i = 0; i < _characterInformation.Count; i++)
        {
            GameObject character;
            character = Instantiate(_characterInformation[i].CharacterPrefab).gameObject;
            if (i == 0)
            {
                character.AddComponent<PlayerCharacter>();
            }
            else
            {
                character.AddComponent<AllyCharacter>();
            }
        }
    }
    
}
