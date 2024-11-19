using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    private StateMachineGenerator _stateMachineGenerator;
    private CharacterDictionary _characterStateDictionary;

    public void GenerateCharacter(ObjectDictionary objectDictionary)
    {
        //キャラクター作るメソッド

        //ここでキャラクターへ情報を格納
        _characterStateDictionary.SetCharacterState(objectDictionary);
    }
}
