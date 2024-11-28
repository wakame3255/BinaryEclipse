using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    public void SetResource(BaseBulletFactory[] bulletFactorys);
    public void DoAttack(Vector3 targetPosition);
}
public interface IDash
{
    public void DoDash();
}
public interface ISkill
{
    public void UseSkill();
}
public interface IWalk
{
    public void DoWalk(float inputX, float inputY, Transform myTransform);
}