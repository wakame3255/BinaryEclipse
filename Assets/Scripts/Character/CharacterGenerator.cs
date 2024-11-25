using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    [SerializeField]
    EnemyDate enemyDate;

    private StateMachineGenerator _stateMachineGenerator;
    private CharacterDictionary _characterStateDictionary;

    public void GenerateCharacter(ObjectDictionary objectDictionary)
    {
        InitializeComponent();
        MyExtensionClass.CheckArgumentNull(objectDictionary, nameof(objectDictionary));
        //�L�����N�^�[��郁�\�b�h

        //�����ŃL�����N�^�[�֏����i�[
        _characterStateDictionary.SetCharacterState(objectDictionary);
        _stateMachineGenerator.InitializeStateMachine(_characterStateDictionary);
    }

    private void InitializeComponent()
    {
        _stateMachineGenerator = new StateMachineGenerator();
        _characterStateDictionary = new CharacterDictionary();
    }
}
