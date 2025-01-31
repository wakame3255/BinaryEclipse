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
            Component enemy;
            BaseCharacter baseCharacter;
            //プレイヤーとキャラクターの生成
            enemy = Instantiate(_characterInformation[i].CharacterPrefab);
          
            baseCharacter = enemy.gameObject.AddComponent<BossCharacter>();

            baseCharacter.SetCharacterStatus(ReturnCharacterStatus(_characterInformation[i]));
            SetCharacterInformation(_characterInformation[i], baseCharacter, enemy);
        }
    }
}
