using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using R3;

public abstract class BaseStateNode : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private protected InNode _inNode;
    [SerializeField]
    private protected OutNode _outNode;
    [SerializeField]
    private protected Image _stateImage;

    private protected ICpuCharacter _cpuCharacter;
    private protected CpuController _cpuController;
    private protected OtherCharacterStatus _otherCharacters;

    private protected bool _isComponentNull = default;

    public InNode InNode { get => _inNode; }
    public OutNode OutNode { get => _outNode; }

    public virtual void EnterState()
    {
        if (_stateImage != null)
        {
            _stateImage.color = Color.red;
        }
    }
    public abstract void UpdateState();
    public virtual void ExitState()
    {
        if (_stateImage != null)
        {
            _stateImage.color = Color.white;
        }
    }

    /// <summary>
    /// 自身が持っているアウトノードを返却
    /// </summary>
    /// <returns></returns>
    public abstract OutNode[] GetHasOutNode();

    private void Start()
    {
        CheckNullComponent();
        if (!_isComponentNull)
        {
            _outNode.SetParentNodeState(this);
        }
    }

    public void SetCharacterInformation(ICpuCharacter cpuCharacter)
    {
        MyExtensionClass.CheckArgumentNull(cpuCharacter, nameof(cpuCharacter));
        _cpuCharacter = cpuCharacter;
    }
    public void SetCpuContoller(CpuController cpuController)
    {
        MyExtensionClass.CheckArgumentNull(cpuController, nameof(cpuController));
        _cpuController = cpuController;
    }
    public void SetOtherCharacterInformation(OtherCharacterStatus otherCharacters)
    {
        MyExtensionClass.CheckArgumentNull(otherCharacters, nameof(otherCharacters));
        _otherCharacters = otherCharacters;
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
            Debug.LogError(transform.name + " " + "子のノードがないよ");
            _isComponentNull = true;
        }
    }
}
