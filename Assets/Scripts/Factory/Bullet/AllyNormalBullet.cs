using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AllyNormalBullet : BaseBullet
{

    public override void GenerateBullet(Vector3 initializePosition, Vector3 targetPosition)
    {
        transform.position = initializePosition;
        transform.rotation = ReturnTargetToAngle(targetPosition);
        _targetDirection = (targetPosition - transform.position); 

        base.StartDestroyTimerAsync();
    }

    public override void MoveBullet()
    {
        transform.position += Vector3.Scale(_targetDirection, new Vector3(1,1,0)).normalized  * _speed * Time.deltaTime;
 
        base.MoveBullet();
    } 
}
