using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterFactory : BaseCharacterFactory
{
    private List<HunterInformation> _hunterCharacters;

    public HunterFactory(List<HunterInformation> hunterCharacter)
    {
        _hunterCharacters = hunterCharacter;
    }

    public override void GenerateCharacter()
    {
        foreach (HunterInformation hunter in _hunterCharacters)
        {
            Instantiate(hunter.HunterPrefab);
        }
    }
    
}
