
using UnityEngine;
using R3;

public interface ICharacterController
{
    public float InputX { get; }
    public float InputY { get; }

    public Vector3 Direction { get; }
    public Transform Target { get; }

    public bool IsAttack { get; }

   public ReactiveProperty<Transform> ReactiveTargetTransform { get; }
}
