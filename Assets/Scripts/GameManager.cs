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
        _objectDictionary = CheckComponentMissing<ObjectDictionary>();
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
        _objectDictionary = CheckComponentMissing<ObjectDictionary>();
    }

    /// <summary>
    /// コンポーネント存在確認。なかった場合はAddを行う
    /// </summary>
    /// <typeparam name="T">チェックの行うコンポーネント</typeparam>
    /// <returns>コンポーネント</returns>
     private T CheckComponentMissing<T>()
    {
        T component;
        if (!TryGetComponent<T>(out component))
        {
            Debug.LogError(transform.name + " " + typeof(T).FullName + "が足りないよ");
            gameObject.AddComponent(typeof(T));
        }
        return component;
    }
}
