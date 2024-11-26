using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Bullet Setting", fileName = "BulletDate")]
public class BulletDate : ScriptableObject
{
    [SerializeField]
    public BaseBullet _baseBulletPrefab;

    [SerializeField]
    public BaseBulletFactory _bulletFactory;
}
