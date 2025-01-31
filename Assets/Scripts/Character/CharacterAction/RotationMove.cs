using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMove : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed;

    private Transform _cacheTransform;
    private float _characterScaleX;

    private void Awake()
    {
        _cacheTransform = transform;
        _characterScaleX = _cacheTransform.localScale.x;
    }

    //���L�̃��\�b�h�͏������������Ă��邩��A���ʉ�������

    /// <summary>
    /// ��]�𐧌䂷�郁�\�b�h
    /// </summary>
    /// <param name="targetPos">�^�[�Q�b�g�̈ʒu</param>
    public void DoRotationMove(Vector3 targetPos)
    {
        if (Mathf.Sign(targetPos.x - _cacheTransform.position.x) > 0)
        {
            _cacheTransform.localScale = new Vector3(_characterScaleX, _cacheTransform.localScale.y, _cacheTransform.localScale.z);
        }
        else
        {
            _cacheTransform.localScale = new Vector3(-_characterScaleX, _cacheTransform.localScale.y, _cacheTransform.localScale.z);
        }
        // _cacheTransform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * targetAngle, Vector3.forward);
    }

    /// <summary>
    /// �^�[�Q�b�g�ɑ΂��Ă�x���猩���p�x
    /// </summary>
    /// <param name="targetPos">�^�[�Q�b�g�̈ʒu</param>
    /// <returns>�^�[�Q�b�g�̂���p�x</returns>
    private float ReturnTargetToAngle(Vector3 targetPos)
    {
        Vector3 targetDirection = targetPos - _cacheTransform.position;

        return Mathf.Atan2(targetDirection.y, targetDirection.x);
    }
}
