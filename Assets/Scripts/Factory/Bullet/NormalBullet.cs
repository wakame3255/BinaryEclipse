using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NormalBullet : BaseBullet
{

    public override void GenerateBullet(Vector3 initializePosition, Vector3 targetDirection)
    {
        transform.position = initializePosition;
        _targetDirection = targetDirection;
        StartDestroyTimerAsync();
    }

    public override void MoveBullet()
    {
        transform.position += (_targetDirection - transform.position).normalized * _speed * Time.deltaTime;
    }

    private async void StartDestroyTimerAsync()
    {
        await Task.Delay(_destroyTime * 1000);
        gameObject.SetActive(false);
    }
}
