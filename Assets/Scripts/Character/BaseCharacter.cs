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

    protected void Awake()
    { 
        SetComponent();
    }

    protected void Start()
    {
        DeliveryValue();
    }

    /// <summary>
    /// ���������̍X�V���s�����\�b�h
    /// </summary>
    public virtual void PhysicsUpDate()
    {
        _collision2D.CheckCollision();
    }

    /// <summary>
    /// �R���|�[�l���g���i�[���郁�\�b�h                  
    /// </summary>
    protected virtual void SetComponent()
    {
        _characterStateView = new CharacterStateView();
        _cacheTransform = this.transform;
        _collision2D = this.CheckComponentMissing<Collision2D>();
        _characterStatus = this.CheckComponentMissing<CharacterStatus>();
        _characterAction = this.CheckComponentMissing<ICharacterAction>();
    }

    /// <summary>
    /// �R���|�l���g���m�̎󂯓n��           
    /// </summary>
    protected abstract void DeliveryValue();
}
