using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IAttack
{
    public void DoAttack();
}
interface IDash
{
    public void DoDash();
}
interface ISkill
{
    public void UseSkill();
}
interface IWalk
{
    public void DoWalk(float inputX, float inputY, Transform myTransform);
}