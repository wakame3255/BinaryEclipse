using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectDictionary))]
public class GameManager : MonoBehaviour
{ 
    private GameObject[] _sceneGameObjects;
    private ObjectDictionary _objectDictionary;

    private BaseCharacter[] _baseCharacter;
    void Start()
    {
        CheckAllComponent();
        SetVariableValue();     
    }

    private void FixedUpdate()
    {
        UpDateCharacter();
    }

    private void Update()
    {
        
    }

    private void CheckAllComponent()
    {
        _objectDictionary = this.CheckComponentMissing<ObjectDictionary>();
    }

    private void UpDateCharacter()
    {
        foreach (BaseCharacter character in _baseCharacter)
        {
            character.PhysicsUpDate();
        }
    }

    private void SetVariableValue()
    {
        _baseCharacter = FindObjectsByType<BaseCharacter>(FindObjectsSortMode.None);
    }
}
