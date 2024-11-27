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
    /// 指定のサブクラスを持ったリストを返すメソッド
    /// </summary>
    /// <typeparam name="T">スーパークラスなど</typeparam>
    /// <param name="baseClass">探し出すリスト</param>
    /// <param name="subClassTypes">探したいサブクラス</param>
    /// <returns>指定のサブクラスを持ったスーパクラス</returns>
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
    /// ディクショナリーに格納するメソッド
    /// </summary>
    /// <param name="gameObjects">すべてのオブジェクト</param>
    private void SetGameObectsInDictionary(IEnumerable<GameObject> gameObjects)
    { 
        MyExtensionClass.CheckArgumentNull(gameObjects, nameof(gameObjects));

        _characterDictionary = GetDictionary<BaseCharacter>(gameObjects);
    }

    /// <summary>
    /// 指定したコンポーネントを持つディクショナリーを返すメソッド
    /// </summary>
    /// <typeparam name="T">値となるコンポーネント</typeparam>
    /// <param name="gameObjects">すべてのオブジェクト</param>
    /// <returns>指定の要素が入ったディクショナリー</returns>
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
