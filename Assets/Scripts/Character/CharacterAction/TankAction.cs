using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnityEngine;

public class TankAction : MonoBehaviour, ICharacterAction
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

        _walk.DoWalk(characterController.InputX, characterController.InputY, _stateView.CharacterTransform);

        //�U������
        if (characterController.IsAttack && _attack != null)
        {
            _attack.DoAttack(characterController.Direction);
        }
    }

    public void SetResourceInformation(CharacterStateView characterState, BaseBulletFactory[] bulletFactorys)
    {
        MyExtensionClass.CheckArgumentNull(characterState, nameof(characterState));
        MyExtensionClass.CheckArgumentNull(bulletFactorys, nameof(bulletFactorys));

        _stateView = characterState;
        _attack.SetResource(bulletFactorys);
    }

    protected void SetComponent()
    {
        _walk = this.CheckComponentMissing<Walk>(_actionObject);
        _attack = this.CheckComponentMissing<ShotAttack>(_actionObject);
    }
}
