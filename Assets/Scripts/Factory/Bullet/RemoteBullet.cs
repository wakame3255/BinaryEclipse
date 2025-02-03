using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RemoteBullet : BaseBullet
{
    public override void GenerateBullet(Vector3 initializePosition, Vector3 targetDirection)
    {

        StartDestroyTimerAsync();
    }

    public override void MoveBullet()
    {
        transform.position += Vector3.down * _speed * Time.deltaTime;
        base.MoveBullet();
    }
}
