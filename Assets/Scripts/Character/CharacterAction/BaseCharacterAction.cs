using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof())]
public abstract class BaseCharacterAction : MonoBehaviour
{

    /// <summary>
    /// �C���v�b�g�������ׂẴA�N�V�����ɓn�����\�b�h
    /// </summary>
    public virtual void SetInput(InputManager inputManager) { }

    /// <summary>
    /// �R���|�[�l���g���݊m�F�B�Ȃ������ꍇ��Add���s��
    /// </summary>
    /// <typeparam name="T">�`�F�b�N�̍s���R���|�[�l���g</typeparam>
    /// <returns>�R���|�[�l���g</returns>
    protected private T CheckComponentMissing<T>()
    {
        T component;
        if (!TryGetComponent<T>(out component))
        {
            Debug.LogError(transform.name + " " + typeof(T).FullName + "������Ȃ���");
            gameObject.AddComponent(typeof(T));
        }
        return component;
    }

    /// <summary>
    /// �R���|�[�l���g���i�[���郁�\�b�h                  
    /// </summary>
    protected virtual void SetComponent() { }
}
