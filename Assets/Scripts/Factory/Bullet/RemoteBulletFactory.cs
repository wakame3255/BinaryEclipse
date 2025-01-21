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
    /// �z�u�ꏊ��Ԃ��Ă���郁�\�b�h
    /// </summary>
    /// <param name="centerPoint">�z�u����ۂ̊�_</param>
    /// <returns>�z�u����ꏊ</returns>
    private List<Vector3> GetShotPoint(Vector3 centerPoint)
    {
        List<Vector3> shotPoints = new List<Vector3>();

        //�u���n�߂�ʒu�ƒu���ۂ̊Ԋu�̎Z�o
        float positionSpan = _xSize / _generateCount;
        Vector3 startSetPosition = centerPoint + (Vector3.left * (_xSize / 2));

        //�z�u�ꏊ�̊i�[
        for (int i = 0; i < _generateCount; i++)
        {
            shotPoints.Add(startSetPosition + (Vector3.right * (positionSpan * i)));
        }

        return shotPoints;
    }
}
