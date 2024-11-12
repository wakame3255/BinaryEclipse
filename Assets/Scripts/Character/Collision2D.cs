using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class Collision2D : MonoBehaviour
{
    [SerializeField]
    private LayerMask _collisionLayerMask;

    [SerializeField]
    private int _maxCollisionCount;

    private CircleCollider2D _circleCollider;
    private RaycastHit2D[] _collisionResults;

    private void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
    }
   
    /// <summary>
    /// 衝突判定確認メソッド
    /// </summary>
    public void CheckCollision()
    {
        _collisionResults = Physics2D.CircleCastAll(transform.position, _circleCollider.radius , Vector2.down, 0f, _collisionLayerMask);

        for (int i = 0; i < _collisionResults.Length; i++)
        {
            ColliderDistance2D repulsionInfomation = ReTurnRepulsionInfomation(_collisionResults[i].collider);

            DoRepulsion(repulsionInfomation);
        }
    }

    /// <summary>
    /// 反発するための情報を返すメソッド
    /// </summary>
    /// <param name="collisionCollider">衝突したコライダー</param>
    /// <returns>反発情報</returns>
    private ColliderDistance2D  ReTurnRepulsionInfomation(Collider2D collisionCollider)
    {               
        ColliderDistance2D colliderDistance = Physics2D.Distance(_circleCollider, collisionCollider);

        return colliderDistance;
    }

    /// <summary>
    /// オブジェクトを反発させるメソッド
    /// </summary>
    /// <param name="repulsionInfomation">ColliderDistance2D</param>
    private void DoRepulsion(ColliderDistance2D repulsionInfomation)
    {
        Vector2 repulsion = (repulsionInfomation.normal * repulsionInfomation.distance);
     
        transform.position += new Vector3(repulsion.x, repulsion.y, 0);
    }
}
