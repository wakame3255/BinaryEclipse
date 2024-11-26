using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyInformation
{
    [SerializeField][Range(0,100)]
    private int _id;

    [SerializeField]
    private int _hp;

    [SerializeField]
    private int _attack;//攻撃力

    [SerializeField]
    private int _defense;

    [SerializeField]
    private BaseCharacter _enemyPrefab; //基礎になる敵のプレハブ

    [SerializeField]
    private GameObject[] _weapons; //使う武器のプレハブ
}

[CreateAssetMenu(menuName = "ScriptableObject/Enemy Setting", fileName = "EnemyDate")]
public class EnemyDate : ScriptableObject
{
    [SerializeField]
    private List<EnemyInformation> _enemyInformation;
}


