using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAction : MonoBehaviour, ICharacterAction
{
    [SerializeField]
    protected private GameObject _actionObject;

    private IWalk _walk;
    private IAttack _attack;
  
    private void Awake()
    {
        SetComponent();
    }

     public void SetInput(ICharacterController characterController, Transform myTransform)
     {
        _walk.DoWalk(characterController.InputX, characterController.InputY, myTransform);

        if (characterController.IsAttack)
        {
            _attack.DoAttack();
        }
     }

    protected void SetComponent()
    {
        _walk = this.CheckComponentMissing<Walk>(_actionObject);
        _attack = this.CheckComponentMissing<ShotAttack>(_actionObject);
    }
}
