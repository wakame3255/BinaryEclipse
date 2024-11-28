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
            BaseCharacter baseCharacter;
            //プレイヤーとキャラクターの生成
            character = Instantiate(_characterInformation[i].CharacterPrefab).gameObject;
            if (i == 0)
            {
                baseCharacter = character.AddComponent<PlayerCharacter>();
            }
            else
            {
                baseCharacter = character.AddComponent<AllyCharacter>();
            }
            SetCharacterInformation(_characterInformation[i], baseCharacter);
        }
    }
    
}
