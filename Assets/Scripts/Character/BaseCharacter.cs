using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision2D))]
[RequireComponent(typeof(CharacterStatus))]
public abstract class BaseCharacter : MonoBehaviour
{
    protected private ICharacterAction _characterAction;
    protected private Collision2D _collision2D;
    protected private CharacterStatus _characterStatus;
    protected private Transform _cacheTransform;
    protected private CharacterStateView _characterStateView;

    public CharacterStateView CharacterStatusView { get => _characterStateView; }
    public CharacterStatus CharacterStatus { get => _characterStatus; }
    public ICharacterAction CharacterAction { get => _characterAction; }


    private void Awake()
    {
        SetComponent();
    }

    /// <summary>
    /// Hpの受け渡し        
    /// </summary>
    public abstract void SetCharacterStatus(CharacterStatus characterStatus);

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
        _collision2D = this.CheckComponentMissing<Collision2D>();
        _characterStatus = this.CheckComponentMissing<CharacterStatus>();
        _characterAction = this.CheckComponentMissing<ICharacterAction>();
    }
}
