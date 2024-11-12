using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CpuController : MonoBehaviour, ICharacterController
{
    [SerializeField]
    private float _inputX;
    [SerializeField]
    private float _inputY;

    [SerializeField]
    private Vector3 _mousePosition;

    [SerializeField]
    private bool _k_key;

    public float InputX { get => _inputX; }
    public float InputY { get => _inputY; }

    public Vector3 Direction { get => _mousePosition; }

    public bool IsAttack { get => _k_key; }

    public void SetInputMove(float inputX, float inputY)
    {
        _inputX = inputX;
        _inputY = inputY;
    }

    public void SetAttack(bool isAttack)
    {
        _k_key = isAttack;
    }
}
