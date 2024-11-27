using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    [SerializeField, Required][Header("�{�X�f�[�^")]
    EnemyDate _enemyDate;

    [SerializeField, Required][Header("�n���^�[�f�[�^")]
    HunterDate _hunterDate;

    private StateMachineGenerator _stateMachineGenerator;
    private CharacterDictionary _characterStateDictionary;
    private EnemyFactory _enemyFactory;
    private HunterFactory _hunterFactory;

    public void GenerateCharacter(ObjectDictionary objectDictionary)
    {
        InitializeComponent();
        MyExtensionClass.CheckArgumentNull(objectDictionary, nameof(objectDictionary));
        //�L�����N�^�[��郁�\�b�h
        _enemyFactory.GenerateCharacter();
        _hunterFactory.GenerateCharacter();
        //�����ŃL�����N�^�[�֏����i�[
        _characterStateDictionary.SetCharacterState(objectDictionary);
        _stateMachineGenerator.InitializeStateMachine(_characterStateDictionary);
    }

    private void InitializeComponent()
    {
        _stateMachineGenerator = new StateMachineGenerator();
        _characterStateDictionary = new CharacterDictionary();
        //�L�����N�^�[�̃f�[�^�Z�b�g
        _enemyFactory = new(_enemyDate.EnemyInformation);
        _hunterFactory = new(_hunterDate.HunterInformation);
    }
}
