using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ShotAttack : MonoBehaviour, IAttack
{
    [SerializeField]
    private int _shotCoolTime;

    //生成した弾のリスト
    private List<IAttackInvoker> _attackInvokers = new List<IAttackInvoker>();

    private int _attackCurrentIndex = default;

    private bool _isCoolingDown = false;

    private void FixedUpdate()
    {
        foreach (IAttackInvoker attack in _attackInvokers)
        {
            attack.UpDateBullet();
        }
    }

    public void DoAttack(Vector3 TargerDirection)
    {
        if (_isCoolingDown) return;

        MyExtensionClass.CheckArgumentNull(TargerDirection, nameof(TargerDirection));

        _attackInvokers[_attackCurrentIndex]?.GenerateAttack(transform.position, TargerDirection);

        UpdateShotIndex();

        StartCoolDownTimerAsync();
    }

    /// <summary>
    /// リソースの初期生成
    /// </summary>
    /// <param name="bulletFactorys">弾の工場</param>
    public void SetResource(BaseBulletFactory[] bulletFactorys)
    {
        MyExtensionClass.CheckArgumentNull(bulletFactorys, nameof(bulletFactorys));

        for (int i = 0; i < bulletFactorys.Length; i++)
        {
            _attackInvokers.Add(bulletFactorys[i].GetAttackInvoker());
        }
    }

    /// <summary>
    /// インデックスの更新
    /// </summary>
    private void UpdateShotIndex()
    {
        _attackCurrentIndex++;
        if (_attackCurrentIndex >= _attackInvokers.Count)
        {
            _attackCurrentIndex = 0;
        }
    }

    private async void StartCoolDownTimerAsync()
    {
        _isCoolingDown = true;
        await Task.Delay(_shotCoolTime * 1000);
        _isCoolingDown = false;
    }
}
