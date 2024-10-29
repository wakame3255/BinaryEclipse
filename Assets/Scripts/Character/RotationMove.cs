using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMove : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed;

    private Transform _cacheTransform;

    private void Start()
    {
        _cacheTransform = transform;
    }
    private void Update()
    {
        //�}�E�X�̍��W���擾����
        Vector3 mousePos = Input.mousePosition;
        //�X�N���[�����W�����[���h���W�ɕϊ�����
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);

        DoRotationMove(pos);
    }

    /// <summary>
    /// ��]�𐧌䂷�郁�\�b�h
    /// </summary>
    /// <param name="targetPos">�^�[�Q�b�g�̈ʒu</param>
    public void DoRotationMove(Vector3 targetPos)
    {
        float targetAngle = ReturnTargetToAngle(targetPos);

        _cacheTransform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * targetAngle, Vector3.forward);
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
