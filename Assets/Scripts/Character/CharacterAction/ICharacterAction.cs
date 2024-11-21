
using UnityEngine;

interface ICharacterAction
{
    /// <summary>
    /// �C���v�b�g�������ׂẴA�N�V�����ɓn�����\�b�h
    /// </summary>
    public void SetInput(ICharacterController inputManager);

    /// <summary>
    /// �l��������transform
    /// </summary>
    /// <param name="characterTransform">�L�����N�^�[�̃g�����X�t�H�[��</param>
    public void SetTransform(Transform characterTransform);
}
