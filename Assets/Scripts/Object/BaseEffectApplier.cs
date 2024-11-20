using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class BaseEffectApplier : MonoBehaviour
{
    [SerializeField]
    private LayerMask _collisionLayerMask;
    [SerializeField]
    private int _coolDownTime;

    private CircleCollider2D _circleCollider;
    private RaycastHit2D[] _collisionResults;

    private bool _canEffectAppli = true;

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
    }
    private void FixedUpdate()
    {
        CheckCollision();
    }

    /// <summary>
    /// 衝突判定確認メソッド
    /// </summary>
    protected private void CheckCollision()
    {
        _collisionResults = Physics2D.CircleCastAll(transform.position, _circleCollider.radius, Vector2.down, 0f, _collisionLayerMask);

        for (int i = 0; i < _collisionResults.Length; i++)
        {
            if (!_canEffectAppli)
            {
                return;
            }

            CharacterStatus characterStatus;
            if (_collisionResults[i].collider.TryGetComponent<CharacterStatus>(out characterStatus))
            {
                AppliEffect(characterStatus);
                CoolDownTime();
            }
        }
    }

    protected abstract void AppliEffect(CharacterStatus characterStatus);

    protected async void CoolDownTime()
    {
        _canEffectAppli = false;

        await Task.Delay(_coolDownTime);

        _canEffectAppli = true;
    }
}
