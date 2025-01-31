
using UnityEngine;

public interface ICharacterAction
{
    /// <summary>
    /// �C���v�b�g�������ׂẴA�N�V�����ɓn�����\�b�h
    /// </summary>
    public void SetControlInformation(ICharacterController inputManager);


    /// <summary>
    /// ���\�[�X�������ׂẴA�N�V�����ɓn�����\�b�h
    /// </summary>
    /// <param name="characterState">�X�e�[�^�X</param>
    /// <param name="bulletFactorys">�U���̎��</param>
    public void SetResourceInformation(CharacterStateView characterState, BaseBulletFactory[] bulletFactorys);
}
