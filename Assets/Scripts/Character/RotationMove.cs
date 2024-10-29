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

    public void DoRotationMove(Vector3 targetPos)
    {
        float targetAngle = ReturnTargetToAngle(targetPos);

        _cacheTransform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * targetAngle, Vector3.forward);

    }

    private float ReturnTargetToAngle(Vector3 targetPos)
    {
        Vector3 targetDirection = targetPos - _cacheTransform.position;

        return Mathf.Atan2(targetDirection.y, targetDirection.x);
    }
}
