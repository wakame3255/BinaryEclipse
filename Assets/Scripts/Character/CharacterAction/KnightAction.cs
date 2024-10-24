using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAction : BaseCharacterAction
{
    IWalk walk;
    private void Start()
    {
        SetComponent();
    }

    private void FixedUpdate()
    {
        walk.DoWalk(InputManager.InputX, InputManager.InputY);
    }

    protected override void SetComponent()
    {
        walk = CheckComponentMissing<IWalk>();
    }
}
