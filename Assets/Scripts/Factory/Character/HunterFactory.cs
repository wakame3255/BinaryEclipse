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
            Component hunter;
            BaseCharacter baseCharacter;
            //プレイヤーとキャラクターの生成
            hunter = Instantiate(_characterInformation[i].CharacterPrefab);
            if (i == 0)
            {             
                baseCharacter = hunter.gameObject.AddComponent<PlayerCharacter>();

            }
            else
            {
                baseCharacter = hunter.gameObject.AddComponent<AllyCharacter>();
            }
            baseCharacter.SetCharacterStatus(ReturnCharacterStatus(_characterInformation[i]));
            SetCharacterInformation(_characterInformation[i], baseCharacter, hunter);
        }
    }
    
}
