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
        List<T> cacheComponent = new List<T>() ;

        foreach(GameObject gameObject in _allObjects)
        {
            T component;
            if (gameObject.TryGetComponent<T>(out component))
            {
                cacheComponent.Add(component);
            }
        }

        return cacheComponent;
    }



    /// <summary>
    /// ディクショナリーに格納するメソッド
    /// </summary>
    /// <param name="gameObjects">すべてのオブジェクト</param>
    private void SetGameObectsInDictionary(IEnumerable<GameObject> gameObjects)
    { 
        MyExtensionClass.CheckArgumentNull(gameObjects, nameof(gameObjects));

        _characterDictionary = RetuneDictionary<BaseCharacter>(gameObjects);
    }

    /// <summary>
    /// 指定したコンポーネントを持つディクショナリーを返すメソッド
    /// </summary>
    /// <typeparam name="T">値となるコンポーネント</typeparam>
    /// <param name="gameObjects">すべてのオブジェクト</param>
    /// <returns>指定の要素が入ったディクショナリー</returns>
    private Dictionary<GameObject, T> RetuneDictionary<T>(IEnumerable<GameObject> gameObjects)
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
