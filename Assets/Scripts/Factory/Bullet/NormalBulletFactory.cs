using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBulletFactory : BaseBulletFactory
{
    public override IAttackInvoker GetAttackInvoker()
    {
        List<BaseBullet> baseBullet = new List<BaseBullet>();
        for (int i = 0; i < _generateCount; i++)
        {
            BaseBullet bullet = Instantiate(_baseBullet);
            bullet.gameObject.SetActive(false);
            baseBullet.Add(bullet);
        }

        return new ShotNormalBullet(baseBullet);
    }
}

public class ShotNormalBullet : IAttackInvoker
{
    private List<BaseBullet> _poolBullet;

    public ShotNormalBullet(List<BaseBullet> baseBullets)
    {
        _poolBullet = baseBullets;
    }

    public void GenerateAttack(Vector3 initializePosition, Vector3 targetDirection)
    {
        foreach (BaseBullet bulletList in _poolBullet)
        {
            if (!bulletList.gameObject.activeSelf)
            {
                bulletList.GenerateBullet(initializePosition, targetDirection);
                return;
            }     
        }
    }

    public void UpDateBullet()
    {
        foreach (BaseBullet baseBullet in _poolBullet)
        {
            if (baseBullet.gameObject.activeSelf)
            {
                baseBullet.MoveBullet();
            }
        }
    }
}
