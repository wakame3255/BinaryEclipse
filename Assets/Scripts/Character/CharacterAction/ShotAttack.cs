using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAttack : MonoBehaviour, IAttack
{
    private BaseBulletFactory[] _bullets;

    public void DoAttack(Vector3 TargerPosition)
    {
        Debug.Log("Shot Attack");
    }

    public void SetResource(BaseBulletFactory[] bulletFactorys)
    {
        _bullets = bulletFactorys;
    }
}
