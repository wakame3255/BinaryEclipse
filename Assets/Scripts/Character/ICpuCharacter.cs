using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICpuCharacter
{
    public StateMachine StateMachine { get; }
    public Transform Transform { get; }
    public CpuController CpuController { get; }
    public StartStateNode StartStateNode { get; }

    public void SetStateMachine(StateMachine stateMachine);
}
