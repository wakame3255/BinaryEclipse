using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBulletFactory : BaseBulletFactory
{
    public override BaseBullet[] GetGenerateBullet()
    {
        BaseBullet[] baseBullet = new BaseBullet[_generateCount];
        for (int i = 0; i < _generateCount; i++)
        {
            baseBullet[i] = Instantiate(_baseBullet);
        }
        return baseBullet;
    }
}
