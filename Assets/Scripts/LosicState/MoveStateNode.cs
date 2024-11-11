using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class MoveStateNode : BaseStateNode

{
    public override void EnterState() { }
    public override void UpdateState()
    {
        _cpuController.SetInputMove(1, 1);
    }
    public override void ExitState() { }
}
