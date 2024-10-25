using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(ObjectDictionary))]
public class GameManager : MonoBehaviour
{
    private GameObject[] sceneGameObjects;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    /// <summary>
    /// �R���|�[�l���g���݊m�F�B�Ȃ������ꍇ��Add���s��
    /// </summary>
    /// <typeparam name="T">�`�F�b�N�̍s���R���|�[�l���g</typeparam>
    /// <returns>�R���|�[�l���g</returns>
    protected private T CheckComponentMissing<T>()
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
