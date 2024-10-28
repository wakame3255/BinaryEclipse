using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMove : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed;

    private void FixedUpdate()
    {
        //マウスの座標を取得する
        Vector3 mousePos = Input.mousePosition;
        //スクリーン座標をワールド座標に変換する
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);

        DoRotationMove(pos);
    }

    public void DoRotationMove(Vector3 mousePos)
    {
        float myPosX = transform.position.x;
        float myPosY = transform.position.y;
        Vector3 targetDirection = new Vector3(mousePos.x - myPosX, mousePos.y - myPosY, 0);

        Quaternion targetQuaternion = Quaternion.LookRotation(targetDirection, transform.up);
        
        Vector3 nowRotation = Quaternion.RotateTowards(transform.rotation, targetQuaternion, _rotationSpeed).eulerAngles;

        transform.rotation = Quaternion.Euler(0, 0, nowRotation.z);
    }
}
