using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class MoveStateNode : BaseStateNode
{
    private Subject<bool> _exitStateSubject = new Subject<bool>();

    public Observable<bool> OnExitState
    {
        get { return _exitStateSubject; }
    }


}
