using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AllyNormalBullet : BaseBullet
{

    public override void GenerateBullet(Vector3 initializePosition, Vector3 targetPosition)
    {
        transform.position = initializePosition;
        _targetDirection = (targetPosition - transform.position);

        StartDestroyTimerAsync();
    }

    public override void MoveBullet()
    {
        transform.position += Vector3.Scale(_targetDirection, new Vector3(1,1,0)).normalized  * _speed * Time.deltaTime;
 
        base.MoveBullet();
    }

    private async void StartDestroyTimerAsync()
    {
        await Task.Delay(_destroyTime * 1000);
        print("gg");
        gameObject.SetActive(false);
    }
}
