using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBulletFactory : BaseBulletFactory
{
    [SerializeField]
    private Transform _centerShotPoint;

    [SerializeField]
    private float _xSize;

    public override IAttackInvoker GetAttackInvoker()
    {
        List<List<BaseBullet>> baseBullets = new List<List<BaseBullet>>();

        List<Vector3> shotPoints = GetShotPoint(_centerShotPoint.position);

        SetShotCount(_generateCount);

        for (int i = 0; i < _generateCount; i++)
        {
            baseBullets.Add(new List<BaseBullet>());

            foreach (Vector3 pos in shotPoints)
            {
                BaseBullet bullet = Instantiate(_baseBullet);
                bullet.transform.position = pos;
                bullet.gameObject.SetActive(false);
                baseBullets[i].Add(bullet);
            }       
        }

        return new ShotRemoteBullet(baseBullets);
    }

    /// <summary>
    /// 配置場所を返してくれるメソッド
    /// </summary>
    /// <param name="centerPoint">配置する際の基準点</param>
    /// <returns>配置する場所</returns>
    private List<Vector3> GetShotPoint(Vector3 centerPoint)
    {
        List<Vector3> shotPoints = new List<Vector3>();

        //置き始める位置と置く際の間隔の算出
        float positionSpan = _xSize / _generateCount;
        Vector3 startSetPosition = centerPoint + (Vector3.left * (_xSize / 2));

        //配置場所の格納
        for (int i = 0; i < _generateCount; i++)
        {
            shotPoints.Add(startSetPosition + (Vector3.right * (positionSpan * i)));
        }

        return shotPoints;
    }
}

public class ShotRemoteBullet : IAttackInvoker
{
    private List<List<BaseBullet>> _poolBullet;

    public ShotRemoteBullet(List<List<BaseBullet>> baseBullets)
    {
        _poolBullet = baseBullets;
    }

    public void GenerateAttack(Vector3 initializePosition, Vector3 targetDirection)
    {
        List<BaseBullet> possibleShotList = ReturnPossibleShotList(_poolBullet);

        foreach (BaseBullet bulletList in possibleShotList)
        {
            Debug.Log("撃ちました");
            bulletList.GenerateBullet(initializePosition, targetDirection);
        }
    }

    public void UpDateBullet()
    {
        foreach (List< BaseBullet > bulletList in _poolBullet)
        {
            if (bulletList.Count <= 0)
            {
                return;
            }

            if(!bulletList[0].gameObject.activeSelf)
            {
                continue;
            }

            foreach (BaseBullet bullet in bulletList)
            {
                bullet.MoveBullet();
            }
        }
    }

    private List<BaseBullet> ReturnPossibleShotList(List<List<BaseBullet>> bulletPool)
    {
        foreach (List<BaseBullet> bulletList  in bulletPool)
        {
            if (!bulletList[0].gameObject.activeSelf)
            {
                Debug.Log(bulletList[0].name);
                return bulletList;
            }
        }

        return null;
    }
}
