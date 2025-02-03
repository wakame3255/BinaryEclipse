using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;
using System;

public enum CharacterType
{
    Hunter,
    Tank,
    Heler,
    Boss
}

[Serializable]
public class CharacterInformation
{
    [SerializeField]
    private CharacterType _characterType;

    [SerializeField]
    private int _hp;

    [Required]
    [SerializeField]
    [InterfaceField(typeof(ICharacterAction))]
    private Component _characterPrefab; //キャラクターの基盤

    [SerializeField, Required]
    private BaseBulletFactory[] _bulletFactorys; //使う武器のプレハブ

    public CharacterType CharacterType { get => _characterType; }
    public int Hp { get => _hp; }
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


