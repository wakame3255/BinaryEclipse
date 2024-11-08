using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterController
{
    public float InputX { get; }
    public float InputY { get; }

    public Vector3 Direction { get; }

    public bool IsAttack { get; }
}
