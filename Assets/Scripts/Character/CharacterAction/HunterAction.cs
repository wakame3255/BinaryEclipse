using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAction : MonoBehaviour, ICharacterAction
{
    IWalk walk;
  
    private void Start()
    {
        SetComponent();
    }

     public void SetInput(ICharacterController characterController)
     {
        walk.DoWalk(characterController.InputX, characterController.InputY);
     }

    protected void SetComponent()
    {
        walk = this.CheckComponentMissing<Walk>();
    }
}
