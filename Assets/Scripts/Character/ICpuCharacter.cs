using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICpuCharacter
{
    public StateMachine StateMachine { get; }

    public Transform Transform { get; }

    public void InitializeStateMachine(CharacterStateDictionary characterState);
}
