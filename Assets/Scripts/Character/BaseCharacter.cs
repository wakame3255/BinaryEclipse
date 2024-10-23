using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision2D))]
[RequireComponent(typeof(CharacterStatus))]
public abstract class BaseCharacter : MonoBehaviour
{
    protected private Collision2D _collision2D;
    protected private CharacterStatus _characterStatus;

    /// <summary>
    /// ���������̍X�V���s�����\�b�h
    /// </summary>
    public void PhysicsUpDate()
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
