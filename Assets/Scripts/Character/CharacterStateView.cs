using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using R3;

[Serializable]
public class CharacterStateView
{
    private int _hp;
    private Transform _targetTransform;
    private Transform _characterTransform;

    public Transform CharacterTransform { get => _characterTransform; }
    public Transform TargetTransform { get => _targetTransform; }
    public int Hp { get => _hp; }

    public void SetComponent(CharacterStatus characterStatus, Transform transform, ICharacterController controller)
    {
        MyExtensionClass.CheckArgumentNull(characterStatus , nameof(characterStatus));
        MyExtensionClass.CheckArgumentNull(transform , nameof(transform));

        _characterTransform = transform;
        controller.ReactiveTargetTransform.Subscribe(transform => _targetTransform = transform);
        characterStatus.ReactiveHp.Subscribe(hp => _hp = hp); 

    }
}
