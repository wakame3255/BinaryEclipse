using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBulletFactory : BaseBulletFactory
{
    [SerializeField]
    private Transform _centerShotPoint;

    [SerializeField]
    private float _xSize;

    public override List<BaseBullet> GetGenerateBullet()
    {
        List<BaseBullet> baseBullet = new List<BaseBullet>();

        List<Vector3> shotPoints = GetShotPoint(_centerShotPoint.position);

        SetShotCount(_generateCount);

        for (int i = 0; i < _generateCount; i++)
        {
            foreach (Vector3 pos in shotPoints)
            {
                BaseBullet bullet = Instantiate(_baseBullet);
                bullet.transform.position = pos;
                bullet.gameObject.SetActive(false);
                baseBullet.Add(bullet);
            }       
        }
        return baseBullet;
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
