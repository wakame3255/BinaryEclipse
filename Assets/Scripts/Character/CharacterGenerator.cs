using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    private StateMachineGenerator _stateMachineGenerator;
    private CharacterDictionary _characterStateDictionary;

    public void GenerateCharacter(ObjectDictionary objectDictionary)
    {
        //�L�����N�^�[��郁�\�b�h

        //�����ŃL�����N�^�[�֏����i�[
        _characterStateDictionary.SetCharacterState(objectDictionary);
    }
}
