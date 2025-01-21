using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ShotAttack : MonoBehaviour, IAttack
{
    [SerializeField]
    private int _shotCoolTime;

    //生成した弾のリスト
    private List<List<BaseBullet>> _bulletList = new List<List<BaseBullet>>();

    private int _cacheShotCount;

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
        List<BaseBullet> baseBullet = GetShotBullet(random, _cacheShotCount);
        print(_cacheShotCount);

        foreach (BaseBullet bullet in baseBullet)
        {
            bullet.GenerateBullet(transform.position, TargerPosition);
        }

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
            _cacheShotCount = bulletFactorys[i].ShotCount;
            print(bulletFactorys[i].name);

            _bulletList.Add(new List<BaseBullet>());
            _bulletList[i] = bulletFactorys[i].GetGenerateBullet();
        }
    }

    /// <summary>
    /// 弾を取得する
    /// </summary>
    /// <param name="index">取得したい弾のインデクス</param>
    /// <returns>撃つことが可能な弾</returns>
    private List<BaseBullet> GetShotBullet(int index, int shotCount)
    {
        List<BaseBullet> baseBullet = new List<BaseBullet>();

        for (int i = 0; i < shotCount; i++)
        {
           baseBullet.Add(_bulletList[index][i]);
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
