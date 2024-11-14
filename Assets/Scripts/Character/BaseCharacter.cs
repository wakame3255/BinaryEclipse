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

    public CharacterStatus CharacterStatus { get => _characterStatus; }

    private void Start()
    {
        SetComponent();
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
        _collision2D = CheckComponentMissing<Collision2D>();
        _characterStatus = CheckComponentMissing<CharacterStatus>();
        _characterAction = CheckComponentMissing<ICharacterAction>();
    }

    /// <summary>
    /// �R���|�[�l���g���݊m�F�B�Ȃ������ꍇ��Add���s��
    /// </summary>
    /// <typeparam name="T">�`�F�b�N�̍s���R���|�[�l���g</typeparam>
    /// <returns>�R���|�[�l���g</returns>
    protected private T CheckComponentMissing<T>()
    {
         T component;
        if(!TryGetComponent<T>(out component))
        {
            Debug.LogError(transform.name + " " + typeof(T).FullName + "������Ȃ���");
            gameObject.AddComponent(typeof(T));
        }
        return component;
    }
}
