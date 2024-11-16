using System.Collections.Generic;
using UnityEngine;

public class ObjectDictionary : MonoBehaviour
{
    private Object[] _sceneGameObjects;

    private Dictionary<GameObject, BaseCharacter> _characterDictionary = new Dictionary<GameObject, BaseCharacter>();

    public Dictionary<GameObject, BaseCharacter> CharacterDictionary { get => _characterDictionary; }

    private void Start()
    {
        _sceneGameObjects = FindObjectsByType(typeof(GameObject), FindObjectsSortMode.None);
        SetGameObectsInDictionary(FindObjectsByType(typeof(GameObject), FindObjectsSortMode.None));
    }

    /// <summary>
    /// �f�B�N�V���i���[�Ɋi�[���郁�\�b�h
    /// </summary>
    /// <param name="gameObjects">���ׂẴI�u�W�F�N�g</param>
    private void SetGameObectsInDictionary(Object[] gameObjects)
    { 
        _characterDictionary = RetuneDictionary<BaseCharacter>(gameObjects);
    }

    /// <summary>
    /// �w�肵���R���|�[�l���g�����f�B�N�V���i���[��Ԃ����\�b�h
    /// </summary>
    /// <typeparam name="T">�l�ƂȂ�R���|�[�l���g</typeparam>
    /// <param name="gameObjects">���ׂẴI�u�W�F�N�g</param>
    /// <returns>�w��̗v�f���������f�B�N�V���i���[</returns>
    private Dictionary<GameObject, T> RetuneDictionary<T>(Object[] gameObjects)
    {
        Dictionary<GameObject, T> saveDictionary = new Dictionary<GameObject, T>();

        foreach (GameObject gameObject in gameObjects)
        {
            T component;

            if (gameObject.TryGetComponent<T>(out component))

                saveDictionary.Add(gameObject, component);
        }

        return saveDictionary;
    }
}
