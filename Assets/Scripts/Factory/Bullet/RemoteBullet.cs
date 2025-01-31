using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class RemoteBullet : BaseBullet
{
    private Vector3 _cacheStartPosition;

    public override void GenerateBullet(Vector3 initializePosition, Vector3 targetDirection)
    {
        _cacheStartPosition = transform.position;

        StartDestroyTimerAsync();
    }

    public override void MoveBullet()
    {
        transform.position += Vector3.down * _speed * Time.deltaTime;
        base.MoveBullet();
    }
}
