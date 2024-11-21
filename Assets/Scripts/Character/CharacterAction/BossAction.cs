using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAction : MonoBehaviour, ICharacterAction
{
    IWalk walk;

    private Transform _transform;

    private void Awake()
    {
        SetComponent();
    }

    public void SetInput(ICharacterController characterController)
    {
        walk.DoWalk(characterController.InputX, characterController.InputY);
    }

    public void SetTransform(Transform characterTransform)
    {
        _transform = characterTransform;
    }

    protected void SetComponent()
    {
        walk = this.CheckComponentMissing<Walk>();
    }
}
