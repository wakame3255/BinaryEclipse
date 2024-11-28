using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    [SerializeField, Required][Header("ボスデータ")]
    CharacterDate _enemyDate;

    [SerializeField, Required][Header("ハンターデータ")]
    CharacterDate _hunterDate;

    private StateMachineGenerator _stateMachineGenerator;
    private CharacterDictionary _characterStateDictionary;
    private EnemyFactory _enemyFactory;
    private HunterFactory _hunterFactory;

    public void GenerateCharacter(ObjectDictionary objectDictionary)
    {
        InitializeComponent();
        MyExtensionClass.CheckArgumentNull(objectDictionary, nameof(objectDictionary));

        //キャラクター作り、リソースを与えるメソッド
        _enemyFactory.GenerateCharacter();
        _hunterFactory.GenerateCharacter();

        //ここでキャラクターへ情報を格納
        _characterStateDictionary.SetCharacterState(objectDictionary);
        _stateMachineGenerator.InitializeStateMachine(_characterStateDictionary);
    }

    private void InitializeComponent()
    {
        _stateMachineGenerator = this.CheckComponentMissing<StateMachineGenerator>();
        _characterStateDictionary = new CharacterDictionary();
        //キャラクターのデータセット
        _enemyFactory = new(_enemyDate.CharacterInformation);
        _hunterFactory = new(_hunterDate.CharacterInformation);
    }
}
