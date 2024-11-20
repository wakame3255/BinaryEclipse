using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAplier : BaseEffectApplier
{
    protected override void AppliEffect(CharacterStatus characterStatus)
    {
        characterStatus.SubtractionHp(1);
    }
}
