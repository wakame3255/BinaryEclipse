using System.Collections.Generic;
using UnityEngine;

public class ObjectDictionary : MonoBehaviour
{
    private Dictionary<GameObject, BaseCharacter> _characterDictionary = new Dictionary<GameObject, BaseCharacter>();

    public Dictionary<GameObject, BaseCharacter> CharacterDictionary { get => _characterDictionary; }

    private void Start()
    {
        SetGameObectsInDictionary(ReturnHasComponent<BaseCharacter>());
    }

    public List<T> ReturnHasComponent<T>()
    {
        List<T> componentsWithT = new List<T>();
        foreach (GameObject obj in FindObjectsByType(typeof(T) ,FindObjectsSortMode.None))
        {
            T component = obj.GetComponent<T>();
            if (component != null)
            {
                componentsWithT.Add(component);
            }
        }
        return componentsWithT;
    }



    /// <summary>
    /// ディクショナリーに格納するメソッド
    /// </summary>
    /// <param name="gameObjects">すべてのオブジェクト</param>
    private void SetGameObectsInDictionary(IEnumerable<Object> gameObjects)
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
    private Dictionary<GameObject, T> RetuneDictionary<T>(IEnumerable<Object> gameObjects)
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
