using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectDictionary : MonoBehaviour
{
    private GameObject[] _allObjects;

    private Dictionary<GameObject, BaseCharacter> _characterDictionary = new Dictionary<GameObject, BaseCharacter>();

    public Dictionary<GameObject, BaseCharacter> CharacterDictionary { get => _characterDictionary; }

    private void Awake()
    {
        _allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
    }
    private void Start()
    {
        SetGameObectsInDictionary(GetHasComponent<GameObject>());
    }

    public List<T> GetHasComponent<T>()
    {
        List<T> cacheComponent = new List<T>();

        UnityEngine.Object[] cacheObject = FindObjectsByType(typeof(T), FindObjectsSortMode.None);

        foreach(UnityEngine.Object gameObject in cacheObject)
        {
            T component;
            if (gameObject.GetType() == typeof(T))
            {
                cacheComponent.Add();
            }
        }

        return cacheComponent;
    }

    /// <summary>
    /// �w��̃T�u�N���X�����������X�g��Ԃ����\�b�h
    /// </summary>
    /// <typeparam name="T">�X�[�p�[�N���X�Ȃ�</typeparam>
    /// <param name="baseClass">�T���o�����X�g</param>
    /// <param name="subClassTypes">�T�������T�u�N���X</param>
    /// <returns>�w��̃T�u�N���X���������X�[�p�N���X</returns>
    public List<T> GetHasSubClass<T>(IEnumerable<T> baseClass, Type subClassTypes)
    {
        MyExtensionClass.CheckArgumentNull(baseClass, nameof(baseClass));
        MyExtensionClass.CheckArgumentNull(subClassTypes, nameof(subClassTypes));

        List<T> cacheComponent = new List<T>();

        foreach (T character in baseClass)
        {
            if (subClassTypes.IsAssignableFrom(character.GetType()))
            {
                cacheComponent.Add(character);
            }
        }
        return cacheComponent;
    }



    /// <summary>
    /// �f�B�N�V���i���[�Ɋi�[���郁�\�b�h
    /// </summary>
    /// <param name="gameObjects">���ׂẴI�u�W�F�N�g</param>
    private void SetGameObectsInDictionary(IEnumerable<GameObject> gameObjects)
    { 
        MyExtensionClass.CheckArgumentNull(gameObjects, nameof(gameObjects));

        _characterDictionary = GetDictionary<BaseCharacter>(gameObjects);
    }

    /// <summary>
    /// �w�肵���R���|�[�l���g�����f�B�N�V���i���[��Ԃ����\�b�h
    /// </summary>
    /// <typeparam name="T">�l�ƂȂ�R���|�[�l���g</typeparam>
    /// <param name="gameObjects">���ׂẴI�u�W�F�N�g</param>
    /// <returns>�w��̗v�f���������f�B�N�V���i���[</returns>
    private Dictionary<GameObject, T> GetDictionary<T>(IEnumerable<GameObject> gameObjects)
    {
        MyExtensionClass.CheckArgumentNull(gameObjects, nameof(gameObjects));

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
