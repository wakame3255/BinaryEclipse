using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision2D))]
[RequireComponent(typeof(CharacterStatus))]
public abstract class BaseCharacter : MonoBehaviour
{

    private Collision2D _collision2D;
    private CharacterStatus _characterStatus;

    protected void Start()
    {
        
    }
    public void PhysicsUpDate()
    {

    }
}
