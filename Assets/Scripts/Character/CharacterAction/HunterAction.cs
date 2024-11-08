using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAction : BaseCharacterAction
{
    IWalk walk;
  
    private void Start()
    {
        SetComponent();
    }

     public override void SetInput(ICharacterController characterController)
     {
        walk.DoWalk(characterController.InputX, characterController.InputY);
     }

    protected override void SetComponent()
    {
        walk = CheckComponentMissing<IWalk>();
    }
}
