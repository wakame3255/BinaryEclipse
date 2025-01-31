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

    //下記のメソッドは処理が似すぎているから、共通化したい

    /// <summary>
    /// 回転を制御するメソッド
    /// </summary>
    /// <param name="targetPos">ターゲットの位置</param>
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
    /// ターゲットに対してのxから見た角度
    /// </summary>
    /// <param name="targetPos">ターゲットの位置</param>
    /// <returns>ターゲットのいる角度</returns>
    private float ReturnTargetToAngle(Vector3 targetPos)
    {
        Vector3 targetDirection = targetPos - _cacheTransform.position;

        return Mathf.Atan2(targetDirection.y, targetDirection.x);
    }
}
