using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class AllyNormalBullet : BaseBullet
{

    public override void GenerateBullet(Vector3 initializePosition, Vector3 targetPosition)
    {
        transform.rotation = ReturnTargetToAngle(targetPosition);
        transform.position = initializePosition;
        _targetDirection = (targetPosition - transform.position); 

        base.StartDestroyTimerAsync();
    }

    public override void MoveBullet()
    {
        transform.position += Vector3.Scale(_targetDirection, new Vector3(1,1,0)).normalized  * _speed * Time.deltaTime;
 
        base.MoveBullet();
    } 
}
