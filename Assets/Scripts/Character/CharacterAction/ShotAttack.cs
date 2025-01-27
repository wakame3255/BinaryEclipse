using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ShotAttack : MonoBehaviour, IAttack
{
    [SerializeField]
    private int _shotCoolTime;

    //生成した弾のリスト
    private List<IAttackInvoker> _attackInvokers = new List<IAttackInvoker>();

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

        int random = Random.Range(0, _attackInvokers.Count);

        _attackInvokers[random].GenerateAttack(transform.position, TargerDirection);

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

    private async void StartCoolDownTimerAsync()
    {
        _isCoolingDown = true;
        await Task.Delay(_shotCoolTime * 1000);
        _isCoolingDown = false;
    }
}
