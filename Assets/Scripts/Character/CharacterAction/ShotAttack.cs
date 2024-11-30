using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ShotAttack : MonoBehaviour, IAttack
{
    [SerializeField]
    private int _shotCoolTime;

    //保持している弾工場のリスト
    private BaseBulletFactory[] _bulletFactorys;

    //生成した弾のリスト
    private List<List<BaseBullet>> _bulletList = new List<List<BaseBullet>>();

    private bool _isCoolingDown = false;

    private void FixedUpdate()
    {
        foreach (List<BaseBullet> bulletList in _bulletList)
        {
            foreach (BaseBullet bullet in bulletList)
            {
                if (bullet.gameObject.activeSelf)
                {
                    bullet.MoveBullet();
                }
            }
        }
    }

    public void DoAttack(Vector3 TargerPosition)
    {
        if (_isCoolingDown) return;

        MyExtensionClass.CheckArgumentNull(TargerPosition, nameof(TargerPosition));

        int random = Random.Range(0, _bulletList.Count);
        BaseBullet baseBullet = GetShotBullet(random);
        if (baseBullet != null)
        {
            baseBullet.GenerateBullet(transform.position, TargerPosition);
            StartCoolDownTimerAsync();
        }
    }

    /// <summary>
    /// リソースの初期生成
    /// </summary>
    /// <param name="bulletFactorys">弾の工場</param>
    public void SetResource(BaseBulletFactory[] bulletFactorys)
    {
        MyExtensionClass.CheckArgumentNull(bulletFactorys, nameof(bulletFactorys));

        _bulletFactorys = bulletFactorys;

        for (int i = 0; i < _bulletFactorys.Length; i++)
        {
            _bulletList.Add(new List<BaseBullet>());
            _bulletList[i] = _bulletFactorys[i].GetGenerateBullet();
        }
    }

    /// <summary>
    /// 弾を取得する
    /// </summary>
    /// <param name="index">取得したい弾のインデクス</param>
    /// <returns>撃つことが可能な弾</returns>
    private BaseBullet GetShotBullet(int index)
    {
        BaseBullet baseBullet = null;

        foreach (BaseBullet bullet in _bulletList[index])
        {
            if (!bullet.gameObject.activeSelf)
            {
                bullet.gameObject.SetActive(true);
                return bullet;
            }
        }

        return baseBullet;
    }

    private async void StartCoolDownTimerAsync()
    {
        _isCoolingDown = true;
        await Task.Delay(_shotCoolTime * 1000);
        _isCoolingDown = false;
    }
}
