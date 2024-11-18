using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class CpuController : MonoBehaviour, ICharacterController
{
    [SerializeField]
    private float _inputX;
    [SerializeField]
    private float _inputY;
    [SerializeField]
    private Vector3 _targetDirection;
    [SerializeField]
    private Transform _targetPosition;
    [SerializeField]
    private bool _k_key;

    private ReactiveProperty<Transform> _reactiveTargetTransform = new ReactiveProperty<Transform>();

    public float InputX { get => _inputX; }
    public float InputY { get => _inputY; }
    public Vector3 Direction { get => _targetDirection; }
    public Transform Target { get => _targetPosition; }
    public bool IsAttack { get => _k_key; }

    public ReactiveProperty<Transform> ReactiveTargetTransform { get => _reactiveTargetTransform; }

    public void SetInputMove(float inputX, float inputY)
    {
        _inputX = inputX;
        _inputY = inputY;
    }

    public void SetAttack(bool isAttack)
    {
        _k_key = isAttack;
    }

    public void SetTargetPosition(Transform targetPos)
    {
        if (targetPos != null)
        {
            _targetPosition = targetPos;
            _targetDirection = targetPos.position;
            _reactiveTargetTransform.Value = targetPos;
        }
    }
}
