using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICharacterAction
{
    /// <summary>
    /// �C���v�b�g�������ׂẴA�N�V�����ɓn�����\�b�h
    /// </summary>
    public void SetInput(ICharacterController inputManager);
}
