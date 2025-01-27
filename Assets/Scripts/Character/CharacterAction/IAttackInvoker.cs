using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackInvoker
{
    public void GenerateAttack(Vector3 initializePosition, Vector3 targetDirection);

    public void UpDateBullet();
}
