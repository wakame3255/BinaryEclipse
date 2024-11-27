using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : BaseCharacterFactory
{
    private List<EnemyInformation> _enemyCharacters;

    public EnemyFactory(List<EnemyInformation> enemyCharacter)
    {
        _enemyCharacters = enemyCharacter;
    }

    public override void GenerateCharacter()
    {
        foreach (EnemyInformation enemy in _enemyCharacters)
        {
            Instantiate(enemy.EnemyPrefab);
        }   
    }
}
