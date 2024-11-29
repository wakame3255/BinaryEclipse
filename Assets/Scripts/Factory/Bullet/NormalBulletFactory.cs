using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBulletFactory : BaseBulletFactory
{
    public override List<BaseBullet>GetGenerateBullet()
    {
        List<BaseBullet> baseBullet = new List<BaseBullet>();
        for (int i = 0; i < _generateCount; i++)
        {
            baseBullet.Add(Instantiate(_baseBullet));
        }
        return baseBullet;
    }
}
