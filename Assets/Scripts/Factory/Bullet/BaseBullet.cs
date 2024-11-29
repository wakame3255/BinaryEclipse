using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField]
    protected int _speed;
    [SerializeField]
    protected int _damage;
    [SerializeField]
    protected int _destroyTime;

    protected Vector3 _targetDirection;

    public abstract void GenerateBullet(Vector3 initializePosition, Vector3 targetDirection);
    public abstract void MoveBullet();
}
