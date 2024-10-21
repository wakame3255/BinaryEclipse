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
    
    public void CheckCollision()
    {
        _collisionResults = Physics2D.CircleCastAll(transform.position, _circleCollider.radius , Vector2.down, 0, _collisionLayerMask);

        for (int i = 0; i < _collisionResults.Length; i++)
        {
            Vector3 direction;
            float distance;

            Physics.ComputePenetration(_circleCollider, transform.position, Quaternion.identity,
                _collisionResults[i].collider, _collisionResults[i].transform.position, _collisionResults[i].transform.eulerAngles,
                out direction, out distance);
        }
    }

    private void SetRepulsionForce(float force)
    {

    }
}
