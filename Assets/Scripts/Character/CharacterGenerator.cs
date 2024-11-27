using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    [SerializeField, Required][Header("ボスデータ")]
    EnemyDate _enemyDate;

    [SerializeField, Required][Header("ハンターデータ")]
    HunterDate _hunterDate;

    private StateMachineGenerator _stateMachineGenerator;
    private CharacterDictionary _characterStateDictionary;
    private EnemyFactory _enemyFactory;
    private HunterFactory _hunterFactory;

    public void GenerateCharacter(ObjectDictionary objectDictionary)
    {
        InitializeComponent();
        MyExtensionClass.CheckArgumentNull(objectDictionary, nameof(objectDictionary));
        //キャラクター作るメソッド
        _enemyFactory.GenerateCharacter();
        _hunterFactory.GenerateCharacter();
        //ここでキャラクターへ情報を格納
        _characterStateDictionary.SetCharacterState(objectDictionary);
        _stateMachineGenerator.InitializeStateMachine(_characterStateDictionary);
    }

    private void InitializeComponent()
    {
        _stateMachineGenerator = new StateMachineGenerator();
        _characterStateDictionary = new CharacterDictionary();
        //キャラクターのデータセット
        _enemyFactory = new(_enemyDate.EnemyInformation);
        _hunterFactory = new(_hunterDate.HunterInformation);
    }
}
