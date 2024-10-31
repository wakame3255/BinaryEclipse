using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InNode : MonoBehaviour
{
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 TargetPos = Input.mousePosition;
        TargetPos.z = 0;
        transform.position = TargetPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public BaseStateNode ReturnStateNode()
    {
        return null;
    }
}
