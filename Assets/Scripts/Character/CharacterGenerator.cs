using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    private StateMachineGenerator _stateMachineGenerator;
    private CharacterStateDictionary _characterStateDictionary;

    public void GenerateCharacter()
    {
        
    }

    public void SetCharacterState(ObjectDictionary objectDictionary)
    {
        _characterStateDictionary.SetCharacterState(objectDictionary);
    }
}
