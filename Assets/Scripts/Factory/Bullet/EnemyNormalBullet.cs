using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyNormalBullet : BaseBullet
{

    public override void GenerateBullet(Vector3 initializePosition, Vector3 targetPosition)
    {
        transform.position = initializePosition;
        _targetDirection = (targetPosition - transform.position).normalized;
        print(_targetDirection);
        StartDestroyTimerAsync();
    }

    public override void MoveBullet()
    {
        transform.position += _targetDirection * _speed * Time.deltaTime;
        base.MoveBullet();
    }

    private async void StartDestroyTimerAsync()
    {
        await Task.Delay(_destroyTime * 1000);
        gameObject.SetActive(false);
    }
}
