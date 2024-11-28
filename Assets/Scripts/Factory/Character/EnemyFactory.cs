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
        foreach (CharacterInformation enemy in _characterInformation)
        {
            Instantiate(enemy.CharacterPrefab);
        }   
    }
}
