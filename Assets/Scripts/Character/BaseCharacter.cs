using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

[RequireComponent(typeof(Collision2D))]
[RequireComponent(typeof(CharacterStatus))]
public abstract class BaseCharacter : MonoBehaviour
{
    protected private ICharacterAction _characterAction;
    protected private Collision2D _collision2D;
    protected private CharacterStatus _characterStatus;
    protected private Transform _cacheTransform;

    [SerializeField]
    protected private CharacterStateView _characterStateView;

    public CharacterStatus CharacterStatus { get => _characterStatus; }
    public CharacterStateView CharacterStatusView { get => _characterStateView; }

    protected void Awake()
    { 
        SetComponent();
    }

    protected void Start()
    {
        DeliveryValue();
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
        _characterStateView = new CharacterStateView();
        _cacheTransform = this.transform;
        _collision2D = CheckComponentMissing<Collision2D>();
        _characterStatus = CheckComponentMissing<CharacterStatus>();
        _characterAction = CheckComponentMissing<ICharacterAction>();
    }

    /// <summary>
    /// コンポネント同士の受け渡し           
    /// </summary>
    protected virtual void DeliveryValue()
    {
        _characterStateView.SetComponent(_characterStatus, this.transform);
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
