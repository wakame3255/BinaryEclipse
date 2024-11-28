using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : BaseCharacterFactory
{
    public EnemyFactory(List<CharacterInformation> enemyCharacter)
    {
        _characterInformation = enemyCharacter;
    }

    public override void GenerateCharacter()
    {
        for (int i = 0; i < _characterInformation.Count; i++)
        {
            GameObject enemy;
            BaseCharacter baseCharacter;
            //プレイヤーとキャラクターの生成
            enemy = Instantiate(_characterInformation[i].CharacterPrefab).gameObject;
            if (i == 0)
            {
                baseCharacter = enemy.AddComponent<PlayerCharacter>();
            }
            else
            {
                baseCharacter = enemy.AddComponent<AllyCharacter>();
            }
            SetCharacterInformation(_characterInformation[i], baseCharacter);
        }
    }
}
