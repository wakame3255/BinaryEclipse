using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterGenerator))]
[RequireComponent(typeof(EventJuge))]
public class GameManager : MonoBehaviour
{ 
    private ObjectDictionary _objectDictionary;
    private CharacterGenerator _characterGenerator;
    private EventJuge _eventJuge;

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
        _eventJuge.JugeEvent();
    }

    private void AddComponents()
    {
        _objectDictionary = gameObject.AddComponent<ObjectDictionary>();
        _characterGenerator = this.CheckComponentMissing< CharacterGenerator >();
        _eventJuge = this.CheckComponentMissing< EventJuge >();
    }

    private void CheckAllComponent()
    {

    }

    private void SetVariableValue()
    {
        _characterGenerator.GenerateCharacter(_objectDictionary);
        _baseCharacter = _objectDictionary.GetHasComponent<BaseCharacter>();
        _eventJuge.SetInformation(_objectDictionary);
    }

    private void UpDateCharacter()
    {
        foreach (BaseCharacter character in _baseCharacter)
        {
            character.PhysicsUpDate();
        }
    }
}
