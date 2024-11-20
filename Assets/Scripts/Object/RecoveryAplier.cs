using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryAplier : BaseEffectApplier
{
    protected override void AppliEffect(CharacterStatus characterStatus)
    {
        characterStatus.AddHp(1);
    }
}
