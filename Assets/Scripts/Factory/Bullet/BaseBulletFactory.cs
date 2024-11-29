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

    public abstract List<BaseBullet> GetGenerateBullet();
}
