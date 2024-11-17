using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    private ObjectDictionary _objectDictionary;
    private CharacterStateDictionary _characterState;

    private BaseCharacter[] _baseCharacter;

    private void Awake()
    {
        AddComponents();
        CheckAllComponent();
    }
    void Start()
    {       
        SetVariableValue();     
    }

    private void FixedUpdate()
    {
        UpDateCharacter();
    }

    private void Update()
    {
        
    }

    private void AddComponents()
    {
        _objectDictionary = gameObject.AddComponent<ObjectDictionary>();
        _characterState = gameObject.AddComponent<CharacterStateDictionary>();
    }

    private void CheckAllComponent()
    {

    }

    private void SetVariableValue()
    {
        _baseCharacter = FindObjectsByType<BaseCharacter>(FindObjectsSortMode.None);
        _characterState.SetCharacterState(_objectDictionary);
    }

    private void UpDateCharacter()
    {
        foreach (BaseCharacter character in _baseCharacter)
        {
            character.PhysicsUpDate();
        }
    }
}
