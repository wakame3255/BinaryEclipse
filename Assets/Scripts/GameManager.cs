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
    /// �R���|�[�l���g���݊m�F�B�Ȃ������ꍇ��Add���s��
    /// </summary>
    /// <typeparam name="T">�`�F�b�N�̍s���R���|�[�l���g</typeparam>
    /// <returns>�R���|�[�l���g</returns>
     private T CheckComponentMissing<T>()
    {
        T component;
        if (!TryGetComponent<T>(out component))
        {
            Debug.LogError(transform.name + " " + typeof(T).FullName + "������Ȃ���");
            gameObject.AddComponent(typeof(T));
        }
        return component;
    }
}
