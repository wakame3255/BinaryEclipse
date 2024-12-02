using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBulletFactory : BaseBulletFactory
{
    [SerializeField]
    private Transform _shotPoint;

    public override List<BaseBullet> GetGenerateBullet()
    {
        List<BaseBullet> baseBullet = new List<BaseBullet>();
        for (int i = 0; i < _generateCount; i++)
        {
            BaseBullet bullet = Instantiate(_baseBullet);
            bullet.gameObject.SetActive(false);
            baseBullet.Add(bullet);
        }
        return baseBullet;
    }
}
