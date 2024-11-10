
using UnityEngine;

public static class MyExtensionClass 
{
    /// <summary>
    /// コンポーネント存在確認。なかった場合はAddを行う
    /// </summary>
    /// <typeparam name="T">チェックの行うコンポーネント</typeparam>
    /// <param name="monoBehaviour">拡張メソッドを呼び出すMonoBehaviour</param>
    /// <returns>コンポーネント</returns>
    public static T CheckComponentMissing<T>(this MonoBehaviour monoBehaviour) where T : Component
    {
        T component;
        if (!monoBehaviour.TryGetComponent<T>(out component))
        {
            Debug.LogError(monoBehaviour.transform.name + " " + typeof(T).FullName + "が足りないよ");
            component = monoBehaviour.gameObject.AddComponent<T>();
        }
        return component;
    }
}
