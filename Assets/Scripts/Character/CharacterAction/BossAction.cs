using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using UnityEngine;

public class BossAction : MonoBehaviour, ICharacterAction
{
    [SerializeField, Required]
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
        MyExtensionClass.CheckArgumentNull(characterController, nameof(characterController));

        if (_walk == null)
        {
            Debug.Log("walk‚È‚¢");
        }
        if (_stateView.CharacterTransform == null)
        {
            Debug.Log("walk‚È‚¢");
            Debug.Log(characterController.InputX);
        }
        _walk.DoWalk(characterController.InputX, characterController.InputY, _stateView.CharacterTransform);

        if (characterController.IsAttack)
        {
            _attack.DoAttack(_stateView.TargetTransform.position);
        }
    }

    public void SetResourceInformation(CharacterStateView characterState, BaseBullet[] bullets)
    {
        _stateView = characterState;
        _attack.SetResource(bullets);
    }

    private void SetComponent()
    {
        _walk = this.CheckComponentMissing<Walk>(_actionObject);
        _attack = this.CheckComponentMissing<ShotAttack>(_actionObject);
    }
}
