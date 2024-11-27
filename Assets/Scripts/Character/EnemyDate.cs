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

    [Required]
    [SerializeField]
    [InterfaceField(typeof(ICharacterAction))]
    private Component _enemyPrefab; //�g������̃v���n�u

    [SerializeField, Required]
    private BaseBulletFactory[] _bullets; //�g������̃v���n�u

    public int ID { get => _id; }
    public int Hp { get => _hp; }
    public int Attack { get => _attack; }
    public Component EnemyPrefab { get => _enemyPrefab; }
    public BaseBulletFactory[] Bullets { get => _bullets; }
}

[CreateAssetMenu(menuName = "ScriptableObject/Character Setting", fileName = "CharacterDate")]
public class EnemyDate : ScriptableObject
{
    [SerializeField]
    private List<EnemyInformation> _enemyInformation;

    public List<EnemyInformation> EnemyInformation { get => _enemyInformation; }
}


