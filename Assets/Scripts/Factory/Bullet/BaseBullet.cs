using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Mathematics;
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
    private Vector3 _resetPosition;
    protected Vector3 _targetDirection;
    private CancellationTokenSource _cancellationTokenSource;

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _cancellationTokenSource = new CancellationTokenSource();
        _resetPosition = transform.position;
    }

    public abstract void GenerateBullet(Vector3 initializePosition, Vector3 targetDirection);
    public virtual void MoveBullet()
    {
        if (gameObject.activeSelf)
        {
            CheckCollision();
        }   
    }

    /// <summary>
    /// 衝突判定確認メソッド
    /// </summary>
    protected void CheckCollision()
    {
        _collisionResults = Physics2D.CircleCastAll(transform.position, _circleCollider.radius, Vector2.down, 0f, _collisionLayerMask);

        for (int i = 0; i < _collisionResults.Length; i++)
        {
            if(_collisionResults[i].collider.TryGetComponent(out  BaseCharacter baseCharacter))
            {
                baseCharacter.CharacterStatus.SubtractionHp(_damage);
                transform.position = _resetPosition;
                gameObject.SetActive(false);
            }
        }
    }

    private protected async void StartDestroyTimerAsync()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        try
        {
            await Task.Delay(_destroyTime * 1000, _cancellationTokenSource.Token);
            if (this != null)
            {
                transform.position = _resetPosition;
                gameObject.SetActive(false);
            }
        }
        catch (TaskCanceledException)
        {
            return;
        }

    }

    /// <summary>
    /// ターゲットに対してのxから見た角度
    /// </summary>
    /// <param name="targetPos">ターゲットの位置</param>
    /// <returns>ターゲットのいる角度</returns>
    protected quaternion ReturnTargetToAngle(Vector3 targetPos)
    {
        Vector3 targetDirection = targetPos - transform.position;

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x);

        return Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.forward);
    }
}
