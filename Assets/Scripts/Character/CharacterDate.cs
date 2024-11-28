using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;
using System;

[Serializable]
public class CharacterInformation
{
    [SerializeField][UnityEngine.Range(0,100)]
    private int _id;

    [SerializeField]
    private int _hp;

    [SerializeField]
    private int _attack;//攻撃力

    [Required]
    [SerializeField]
    [InterfaceField(typeof(ICharacterAction))]
    private Component _characterPrefab; //キャラクターの基盤

    [SerializeField, Required]
    private BaseBulletFactory[] _bulletFactorys; //使う武器のプレハブ

    public int ID { get => _id; }
    public int Hp { get => _hp; }
    public int Attack { get => _attack; }
    public Component CharacterPrefab { get => _characterPrefab; }
    public BaseBulletFactory[] BulletFactorys { get => _bulletFactorys; }
}

[CreateAssetMenu(menuName = "ScriptableObject/Character Setting", fileName = "CharacterDate")]
public class CharacterDate : ScriptableObject
{
    [SerializeField]
    private List<CharacterInformation> _characterInformation;

    public List<CharacterInformation> CharacterInformation { get => _characterInformation; }
}


