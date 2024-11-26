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
    private int _attack;//�U����

    [SerializeField]
    private int _defense;

    [SerializeField]
    private BaseCharacter _enemyPrefab; //��b�ɂȂ�G�̃v���n�u

    [SerializeField]
    private GameObject[] _weapons; //�g������̃v���n�u
}

[CreateAssetMenu(menuName = "ScriptableObject/Enemy Setting", fileName = "EnemyDate")]
public class EnemyDate : ScriptableObject
{
    [SerializeField]
    private List<EnemyInformation> _enemyInformation;
}


