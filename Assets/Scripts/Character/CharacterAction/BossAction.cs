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

    private int _count = 0;

    private void Awake()
    {
        SetComponent();
    }

    public void SetControlInformation(ICharacterController characterController)
    {
        MyExtensionClass.CheckArgumentNull(characterController, nameof(characterController));

        if (_stateView.Hp > 0)
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

    public void SetResourceInformation(CharacterStateView characterState, BaseBulletFactory[] bulletFactorys)
    {
        MyExtensionClass.CheckArgumentNull(characterState, nameof(characterState));
        MyExtensionClass.CheckArgumentNull(bulletFactorys, nameof(bulletFactorys));
        _count++;
        print(_attack + " " + _count);
        _stateView = characterState;
        _attack.SetResource(bulletFactorys);
    }

    private void SetComponent()
    {
        _walk = this.CheckComponentMissing<Walk>(_actionObject);
        _attack = this.CheckComponentMissing<ShotAttack>(_actionObject);
        _count++;
        print(_attack +" "+ _count);
    }
}
