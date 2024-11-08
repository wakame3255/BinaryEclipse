using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision2D))]
[RequireComponent(typeof(CharacterStatus))]
public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField]
    protected private BaseCharacterAction _characterAction;

    protected private Collision2D _collision2D;
    protected private CharacterStatus _characterStatus;

    private void Start()
    {
        SetComponent();
    }

    /// <summary>
    /// 物理挙動の更新を行うメソッド
    /// </summary>
    public virtual void PhysicsUpDate()
    {
        _collision2D.CheckCollision();
    }

    /// <summary>
    /// コンポーネントを格納するメソッド                  
    /// </summary>
    protected virtual void SetComponent()
    {
        _collision2D = CheckComponentMissing<Collision2D>();
        _characterStatus = CheckComponentMissing<CharacterStatus>();
    }

    /// <summary>
    /// コンポーネント存在確認。なかった場合はAddを行う
    /// </summary>
    /// <typeparam name="T">チェックの行うコンポーネント</typeparam>
    /// <returns>コンポーネント</returns>
    protected private T CheckComponentMissing<T>()
    {
         T component;
        if(!TryGetComponent<T>(out component))
        {
            Debug.LogError(transform.name + " " + typeof(T).FullName + "が足りないよ");
            gameObject.AddComponent(typeof(T));
        }
        return component;
    }
}
