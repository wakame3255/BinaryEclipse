using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField]
    protected int _speed;
    [SerializeField]
    protected int _damage;
    [SerializeField]
    protected int _destroyTime;
    [SerializeField]
    private LayerMask _collisionLayerMask;

    private CircleCollider2D _circleCollider;
    private RaycastHit2D[] _collisionResults;
    protected Vector3 _targetDirection;

    public abstract void GenerateBullet(Vector3 initializePosition, Vector3 targetDirection);
    public virtual void MoveBullet()
    {
        CheckCollision();
    }

    /// <summary>
    /// 衝突判定確認メソッド
    /// </summary>
    protected void CheckCollision()
    {
        _collisionResults = Physics2D.CircleCastAll(transform.position, _circleCollider.radius, Vector2.down, 0f, _collisionLayerMask);

        for (int i = 0; i < _collisionResults.Length; i++)
        {
            if(_collisionResults[i].collider.TryGetComponent(out CharacterStatus characterStatus))
            {
                characterStatus.SubtractionHp(_damage);
                gameObject.SetActive(false);
            }
        }
    }
}
