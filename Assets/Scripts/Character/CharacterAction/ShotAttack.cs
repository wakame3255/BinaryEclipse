using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAttack : MonoBehaviour, IAttack
{
    //保持している弾工場のリスト
    private BaseBulletFactory[] _bulletFactorys;

    //生成した弾のリスト
    private List<List<BaseBullet>> _bulletList = new List<List<BaseBullet>>();

    public void DoAttack(Vector3 TargerPosition)
    {
        int random = Random.Range(0, _bulletList.Count);
        GetShotBullet(random).GenerateBullet(transform.position, TargerPosition);
        print("ShotAttack");
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
                return bullet;
            }
        }

        return baseBullet;
    }
}
