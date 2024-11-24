using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAttack : MonoBehaviour, IAttack
{
    public void DoAttack()
    {
        Debug.Log("Shot Attack");
    }
}
