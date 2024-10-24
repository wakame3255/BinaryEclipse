using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof())]
public abstract class BaseCharacterAction : MonoBehaviour
{

    /// <summary>
    /// インプット情報をすべてのアクションに渡すメソッド
    /// </summary>
    public virtual void SetInput(InputManager inputManager) { }

    /// <summary>
    /// コンポーネント存在確認。なかった場合はAddを行う
    /// </summary>
    /// <typeparam name="T">チェックの行うコンポーネント</typeparam>
    /// <returns>コンポーネント</returns>
    protected private T CheckComponentMissing<T>()
    {
        T component;
        if (!TryGetComponent<T>(out component))
        {
            Debug.LogError(transform.name + " " + typeof(T).FullName + "が足りないよ");
            gameObject.AddComponent(typeof(T));
        }
        return component;
    }

    /// <summary>
    /// コンポーネントを格納するメソッド                  
    /// </summary>
    protected virtual void SetComponent() { }
}
