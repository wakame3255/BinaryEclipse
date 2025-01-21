using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

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

    private async void StartDestroyTimerAsync()
    {
        await Task.Delay(_destroyTime * 1000);
        transform.position = _cacheStartPosition;
        gameObject.SetActive(false);
    }
}
