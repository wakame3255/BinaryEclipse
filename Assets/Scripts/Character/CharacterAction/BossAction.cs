using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAction : MonoBehaviour, ICharacterAction
{
    [SerializeField]
    protected private GameObject _actionObject;

    private CharacterStateView _stateView;
    private IWalk _walk;
    private IAttack _attack;

    private void Awake()
    {
        SetComponent();
    }

    public void SetControlInformation(ICharacterController characterController)
    {
        _walk.DoWalk(characterController.InputX, characterController.InputY, _stateView.CharacterTransform);

        if (characterController.IsAttack)
        {
            _attack.DoAttack(_stateView.TargetTransform.position);
        }
    }

    public void SetResourceInformation(CharacterStateView characterState)
    {
        _stateView = characterState;
    }

    private void SetComponent()
    {
        _walk = this.CheckComponentMissing<Walk>(_actionObject);
        _attack = this.CheckComponentMissing<ShotAttack>(_actionObject);
    }
}
