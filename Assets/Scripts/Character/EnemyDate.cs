using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;
using System;

[Serializable]
public class EnemyInformation
{
    [SerializeField][UnityEngine.Range(0,100)]
    private int _id;

    [SerializeField]
    private int _hp;

    [SerializeField]
    private int _attack;//攻撃力

    [SerializeField, Required]
    private BaseCharacter _enemyPrefab; //基礎になる敵のプレハブ

    [SerializeField, Required]
    private BaseBulletFactory[] _bullets; //使う武器のプレハブ
}

[CreateAssetMenu(menuName = "ScriptableObject/Enemy Setting", fileName = "EnemyDate")]
public class EnemyDate : ScriptableObject
{
    [SerializeField]
    private List<EnemyInformation> _enemyInformation;
}


