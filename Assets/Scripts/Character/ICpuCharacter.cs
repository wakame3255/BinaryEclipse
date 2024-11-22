using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICpuCharacter
{
    public Cpu.StateMachine StateMachine { get; }
    public Transform Transform { get; }
    public CpuController CpuController { get; }
    public StartStateNode StartStateNode { get; }

    public void SetStateMachine(Cpu.StateMachine stateMachine);
}
