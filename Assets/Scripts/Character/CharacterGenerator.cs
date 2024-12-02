using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;

[RequireComponent(typeof(StateMachineGenerator))]
[RequireComponent(typeof(UIGenerater))]
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
    private UIGenerater _uIGenerater;
    public void GenerateCharacter(ObjectDictionary objectDictionary)
    {
        InitializeComponent();
        MyExtensionClass.CheckArgumentNull(objectDictionary, nameof(objectDictionary));

        //�L�����N�^�[���A���\�[�X��^���郁�\�b�h
        _enemyFactory.GenerateCharacter();
        _hunterFactory.GenerateCharacter();

        //�����ŃL�����N�^�[�֏����i�[
        _characterStateDictionary.SetCharacterState(objectDictionary);
        _stateMachineGenerator.InitializeStateMachine(_characterStateDictionary, objectDictionary);
        _uIGenerater.UIGenerate(_characterStateDictionary);
    }

    private void InitializeComponent()
    {
        _stateMachineGenerator = this.CheckComponentMissing<StateMachineGenerator>();
        _uIGenerater = this.CheckComponentMissing<UIGenerater>();
        _characterStateDictionary = new CharacterDictionary();
        
        //�L�����N�^�[�̃f�[�^�Z�b�g
        _enemyFactory = new(_enemyDate.CharacterInformation);
        _hunterFactory = new(_hunterDate.CharacterInformation);
        
    }
}
