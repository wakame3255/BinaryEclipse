using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : BaseBullet
{
    public override void GenerateBullet(Vector3 initializePosition, Vector3 targetDirection)
    {
        transform.position = initializePosition;
        transform.right = targetDirection;
    }
    public override void MoveBullet()
    {
        transform.position += transform.right * _speed * Time.deltaTime;
    }
}
