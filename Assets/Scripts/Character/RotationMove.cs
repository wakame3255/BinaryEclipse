using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMove : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed;

    private void FixedUpdate()
    {
        //�}�E�X�̍��W���擾����
        Vector3 mousePos = Input.mousePosition;
        //�X�N���[�����W�����[���h���W�ɕϊ�����
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);

        DoRotationMove(pos);
    }

    public void DoRotationMove(Vector3 mousePos)
    {
        float myPosX = transform.position.x;
        float myPosY = transform.position.y;
        Vector2 myRotation2D = new Vector2(myPosX, myPosY);
        Vector2 targetDirection = new Vector2(mousePos.x - myPosX, mousePos.y - myPosY);

        Quaternion quaternion = Quaternion.LookRotation(targetDirection, transform.up);

        Quaternion quaternion1 = Quaternion.RotateTowards(transform.rotation, quaternion, _rotationSpeed);
    }
}
