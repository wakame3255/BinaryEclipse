using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBulletFactory : BaseBulletFactory
{
    public override List<BaseBullet>GetGenerateBullet()
    {
        List<BaseBullet> baseBullet = new List<BaseBullet>(_generateCount);
        for (int i = 0; i < _generateCount; i++)
        {
            baseBullet[i] = Instantiate(_baseBullet);
        }
        return baseBullet;
    }
}
