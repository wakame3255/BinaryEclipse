using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using R3;

 public abstract class BaseStateNode : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private protected InNode _inNode;
    [SerializeField]
    private protected OutNode _outNode;

    private protected BaseCharacter _baseCharacter;

    private protected bool _isComponentNull = default;

    public InNode InNode { get => _inNode; }
    public OutNode OutNode { get => _outNode; }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();

    private void Start()
    {
        CheckNullComponent();
        if (!_isComponentNull)
        {
            _outNode.SetParentNodeState(this);
        }
    }

    public void SetStateNode(BaseCharacter baseCharacter)
    {
        _baseCharacter = baseCharacter;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 TargetPos = Input.mousePosition;
        TargetPos.z = 0;
        transform.position = TargetPos;
    }

    private void CheckNullComponent()
    {
        if (_inNode == null || _outNode == null)
        {
            Debug.LogError(transform.root.name + " " + "éqÇÃÉmÅ[ÉhÇ™Ç»Ç¢ÇÊ");
            _isComponentNull = true;
        }
    }
}
