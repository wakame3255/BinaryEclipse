using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidanceDirectionCheck
{
    private LayerMask _walllayerMask;
   
    private LayerMask _ammoLayerMask;

    private Transform _characterTransform;

    private Vector2[] _cacheDirection;

    public AvoidanceDirectionCheck(LayerMask wallLayer, LayerMask ammoLayer, Transform characterPos)
    {
        _walllayerMask = wallLayer;
        _ammoLayerMask = ammoLayer;
        _characterTransform = characterPos;

        _cacheDirection = new Vector2[] { Vector2.up, Vector2.down, Vector2.right, Vector2.left };
    }

    public Vector2 ReturnMoveDirection()
    {
        Vector2 characterPosition = new(_characterTransform.position.x, _characterTransform.position.y);

        //int directionIndex = ReturnLongDistanceCache(characterPosition);

        Vector2 avoidanceDirection = ReturnAmmoDirection(characterPosition);

        return avoidanceDirection;
    }

    public Vector2 ReturnAmmoDirection(Vector2 characterPosition)
    {
        Vector2 cacheDirection = Vector2.zero;
        float cacheDistance = 1000f;

        RaycastHit2D[] hit2Ds = Physics2D.CircleCastAll(characterPosition, 100f, Vector2.up, 0.1f, _ammoLayerMask);
        foreach (RaycastHit2D hit in hit2Ds)
        {
            if (Vector3.Distance(hit.transform.position, _characterTransform.position) < cacheDistance)
            {
                cacheDirection = -(new Vector2(hit.transform.position.x, hit.transform.position.y) - characterPosition);
            }
        }

        return cacheDirection.normalized;
    }

    private int ReturnLongDistanceCache(Vector2 characterPosition)
    {
        int cacheAllayNumber = 0;
        float cacheDistance = 0f;

        for (int i = 0; i < _cacheDirection.Length; i++)
        {
           RaycastHit2D hit = Physics2D.Raycast(characterPosition, _cacheDirection[i], float.PositiveInfinity, _walllayerMask);
            if (hit.distance > cacheDistance)
            {
                cacheAllayNumber = i;
                cacheDistance = hit.distance;
            }
        }

        return cacheAllayNumber;
    }
}
