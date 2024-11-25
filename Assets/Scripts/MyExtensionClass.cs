
using UnityEngine;
using UnityEngine.DedicatedServer;

public static class MyExtensionClass 
{
    /// <summary>
    /// コンポーネント存在確認。なかった場合はAddを行う
    /// </summary>
    /// <typeparam name="T">チェックの行うコンポーネント</typeparam>
    /// <param name="monoBehaviour">拡張メソッドを呼び出すMonoBehaviour</param>
    /// <returns>コンポーネント</returns>
    public static T CheckComponentMissing<T>(this MonoBehaviour monoBehaviour, GameObject gameObject = null)
    {
        T component;
        if (!monoBehaviour.TryGetComponent<T>(out component))
        {
            Debug.LogError(monoBehaviour.transform.name + " " + typeof(T).FullName + "が足りないよ");

            //付けたいオブジェクトの指定
            if (gameObject == null)
            {
                monoBehaviour.gameObject.AddComponent(typeof(T));
            }
            else
            {
                gameObject.AddComponent(typeof(T));
            }
        }
        return component;
    }

    /// <summary>
    /// 引数がnullかどうかをチェックするメソッド
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="arugment">引数</param>
    /// <exception cref="System.ArgumentNullException"></exception>
    public static void CheckArgumentNull<T>(T arugment, string arugmentName)
    {
        if (arugment == null)
        {
            throw new System.ArgumentNullException(arugmentName);
        }
    }
}
