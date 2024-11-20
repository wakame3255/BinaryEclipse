using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    private ObjectDictionary _objectDictionary;
    private CharacterGenerator _characterGenerator;

    private List<BaseCharacter> _baseCharacter;

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
        _characterGenerator = new CharacterGenerator();
    }

    private void CheckAllComponent()
    {

    }

    private void SetVariableValue()
    {
        _characterGenerator.GenerateCharacter(_objectDictionary);
        _baseCharacter = _objectDictionary.GetHasComponent<BaseCharacter>();
    }

    private void UpDateCharacter()
    {
        foreach (BaseCharacter character in _baseCharacter)
        {
            character.PhysicsUpDate();
        }
    }
}
