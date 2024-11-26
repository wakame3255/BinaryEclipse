using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;

public abstract class BaseBulletFactory : MonoBehaviour
{
    [SerializeField, Required]
    protected BaseBullet _baseBullet;

    public abstract BaseBullet GetGenerateBullet();
}
