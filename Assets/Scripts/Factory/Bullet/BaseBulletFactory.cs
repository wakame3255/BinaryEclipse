using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;

public abstract class BaseBulletFactory : MonoBehaviour
{
    [SerializeField]
    protected int _generateCount;

    [SerializeField, Required]
    protected BaseBullet _baseBullet;

    public int ShotCount { get; private set; } = 1;

    public abstract List<BaseBullet> GetGenerateBullet();

    private protected void SetShotCount(int shotCount)
    {
        ShotCount = shotCount;
    }
}
