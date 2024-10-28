using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(ObjectDictionary))]
public class GameManager : MonoBehaviour
{ 
    private GameObject[] _sceneGameObjects;
    private ObjectDictionary _objectDictionary;

    private BaseCharacter _baseCharacter;
    void Start()
    {
        _sceneGameObjects = FindObjectsByType<GameObject>(sortMode: FindObjectsSortMode.None);

        CheckAllComponent();
        CallStartComponent();
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        
    }

    private void CheckAllComponent()
    {
        _objectDictionary = CheckComponentMissing<ObjectDictionary>();
    }

    private void CallStartComponent()
    {
        
    }

    private void SetVariableValue()
    {

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
