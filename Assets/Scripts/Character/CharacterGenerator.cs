using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    [SerializeField, Required][Header("�{�X�f�[�^")]
    CharacterDate _enemyDate;

    [SerializeField, Required][Header("�n���^�[�f�[�^")]
    CharacterDate _hunterDate;

    private StateMachineGenerator _stateMachineGenerator;
    private CharacterDictionary _characterStateDictionary;
    private EnemyFactory _enemyFactory;
    private HunterFactory _hunterFactory;

    public void GenerateCharacter(ObjectDictionary objectDictionary)
    {
        InitializeComponent();
        MyExtensionClass.CheckArgumentNull(objectDictionary, nameof(objectDictionary));

        //�L�����N�^�[���A���\�[�X��^���郁�\�b�h
        _enemyFactory.GenerateCharacter();
        _hunterFactory.GenerateCharacter();

        //�����ŃL�����N�^�[�֏����i�[
        _characterStateDictionary.SetCharacterState(objectDictionary);
        _stateMachineGenerator.InitializeStateMachine(_characterStateDictionary);
    }

    private void InitializeComponent()
    {
        _stateMachineGenerator = this.CheckComponentMissing<StateMachineGenerator>();
        _characterStateDictionary = new CharacterDictionary();
        //�L�����N�^�[�̃f�[�^�Z�b�g
        _enemyFactory = new(_enemyDate.CharacterInformation);
        _hunterFactory = new(_hunterDate.CharacterInformation);
    }
}
