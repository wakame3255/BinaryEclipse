using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

 public class BaseStateNode : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private protected InNode _inNode;
    [SerializeField]
    private protected OutNode _outNode;

    public InNode InNode { get => _inNode; }
    public OutNode OutNode { get => _outNode; }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 TargetPos = Input.mousePosition;
        TargetPos.z = 0;
        transform.position = TargetPos;
    }
}
