using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    protected int _speed;
    protected int _damage;

    public abstract void GenerateBullet(Vector3 initializePosition, Vector3 targetDirection);
    public abstract void MoveBullet();
}
