using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using R3;

[Serializable]
public class CharacterStateView
{
    private int _hp;
    private Transform _characterTransform;

    public Transform CharacterTransform { get => _characterTransform; }
    public int Hp { get => _hp; }

    public void SetComponent(CharacterStatus characterStatus, Transform transform)
    {
        MyExtensionClass.CheckArgumentNull(characterStatus , nameof(characterStatus));
        MyExtensionClass.CheckArgumentNull(transform , nameof(transform));

        _characterTransform = transform;
        characterStatus.ReactiveHp.Subscribe(hp => _hp = hp); 
    }
}
