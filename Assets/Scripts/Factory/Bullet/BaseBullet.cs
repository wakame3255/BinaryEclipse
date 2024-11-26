using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    protected int _speed;
    protected int _damage;

    public abstract void GenerateBullet();
    public abstract void MoveBullet();
}
