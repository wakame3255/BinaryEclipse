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
    private int _attack;//�U����

    [SerializeField, Required]
    private BaseCharacter _enemyPrefab; //��b�ɂȂ�G�̃v���n�u

    [SerializeField, Required]
    private BaseBulletFactory[] _bullets; //�g������̃v���n�u
}

[CreateAssetMenu(menuName = "ScriptableObject/Enemy Setting", fileName = "EnemyDate")]
public class EnemyDate : ScriptableObject
{
    [SerializeField]
    private List<EnemyInformation> _enemyInformation;
}


