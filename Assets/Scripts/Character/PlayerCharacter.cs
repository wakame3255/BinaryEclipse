using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField]
    BaseCharacterAction _characterAction;

    public override void PhysicsUpDate()
    {
        base.PhysicsUpDate();
    }
}
