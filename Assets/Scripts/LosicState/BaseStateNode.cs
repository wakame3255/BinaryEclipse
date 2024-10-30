using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

 public class BaseStateNode : MonoBehaviour, IDragHandler
{
    private protected BaseNode _inNode;
    private protected BaseNode _outNode;

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 TargetPos = Input.mousePosition;
        TargetPos.z = 0;
        transform.position = TargetPos;
    }
}
