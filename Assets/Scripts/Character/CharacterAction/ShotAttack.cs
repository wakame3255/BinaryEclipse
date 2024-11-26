using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAttack : MonoBehaviour, IAttack
{
    private BaseBullet[] _bullets;

    public void DoAttack(Vector3 TargerPosition)
    {
        Debug.Log("Shot Attack");
    }

    public void SetResource(BaseBullet[] bullets)
    {
        _bullets = bullets;
    }
}
