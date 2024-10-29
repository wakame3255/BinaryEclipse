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
        //マウスの座標を取得する
        Vector3 mousePos = Input.mousePosition;
        //スクリーン座標をワールド座標に変換する
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);

        DoRotationMove(pos);
    }

    /// <summary>
    /// 回転を制御するメソッド
    /// </summary>
    /// <param name="targetPos">ターゲットの位置</param>
    public void DoRotationMove(Vector3 targetPos)
    {
        float targetAngle = ReturnTargetToAngle(targetPos);

        _cacheTransform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * targetAngle, Vector3.forward);
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
