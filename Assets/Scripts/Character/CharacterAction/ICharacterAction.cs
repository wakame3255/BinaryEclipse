
using UnityEngine;

public interface ICharacterAction
{
    /// <summary>
    /// �C���v�b�g�������ׂẴA�N�V�����ɓn�����\�b�h
    /// </summary>
    public void SetControlInformation(ICharacterController inputManager);

    public void SetResourceInformation(CharacterStateView characterState);
}
