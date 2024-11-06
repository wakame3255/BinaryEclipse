using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

[RequireComponent(typeof(CircleCollider2D))]
public class OutNode : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private UILineConnector _lineRenderer;
    [SerializeField]
    private RectTransform _rectOriginPosition;
    private RectTransform _rectMyTransform;

    [SerializeField]
    private LayerMask _collisionLayerMask;

    private CircleCollider2D _circleCollider;
    private RaycastHit2D[] _collisionResults;

    private BaseStateNode _nextStateNode;
    private BaseStateNode _myParentStateNode = default;
    private bool _isConect = default;
    private bool _isComponentNull = default;

    public RectTransform MyRectTransform { get => _rectMyTransform; }
    public BaseStateNode NextNodeState { get => _nextStateNode; }
    public bool IsConect { get => _isConect; }
    private void Start()
    {
        _rectMyTransform = transform as RectTransform;
        CheckComponentNull();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 TargetPos = Input.mousePosition;
        TargetPos.z = 0;
        _rectMyTransform.position = TargetPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isComponentNull)
        {
            CheckCollisionNode();
        }  
    }

    public void SetParentNodeState(BaseStateNode stateNode)
    {
        _myParentStateNode = stateNode;
    }

    private void CheckCollisionNode()
    {
        _collisionResults = Physics2D.CircleCastAll(transform.position, _circleCollider.radius, Vector2.down, 0, _collisionLayerMask);

        for (int i = 0; i < _collisionResults.Length; i++)
        {
            if (CheckHasComponent(_collisionResults[i]))
            {
                return;
            }
        }
        ResetNode();
    }

    /// <summary>
    /// BaseStateを持っているか判定を行うメソッド
    /// </summary>
    /// <param name="raycastHit">当たったオブジェクト</param>
    /// <returns>BaseStateを見つけたかどうか</returns>
    private bool CheckHasComponent(RaycastHit2D raycastHit)
    {
        bool hasComponent = default;
        BaseStateNode stateNode;

        if (raycastHit.collider.TryGetComponent<BaseStateNode>(out stateNode))
        {
            if (stateNode != _myParentStateNode)
            { 
                hasComponent = true;
                UpDateConectState(stateNode);
            }
        }
        else
        {
            hasComponent = false;
            ResetNode();
        }

        _isConect = hasComponent;
        print(hasComponent + " " + raycastHit.collider.name);
        return hasComponent;
    }

    /// <summary>
    /// ノード接続情報の更新
    /// </summary>
    /// <param name="stateNode">取得したBaseNode</param>
    private void UpDateConectState(BaseStateNode stateNode)
    {
        transform.position = _rectOriginPosition.TransformPoint(_rectOriginPosition.anchoredPosition3D);
        _nextStateNode = stateNode;

        _lineRenderer.transforms = new RectTransform[]
        {
            _rectOriginPosition,
            stateNode.InNode.RectMyTransform
        };
    }

    /// <summary>
    /// ノード接続のリセット
    /// </summary>
    private void ResetNode()
    {
        transform.position = _rectOriginPosition.TransformPoint(_rectOriginPosition.anchoredPosition3D);
        _nextStateNode = null;
        _isConect = false;

        _lineRenderer.transforms = new RectTransform[]
       {
            _rectOriginPosition,
            _rectMyTransform
       };
    }

    private void CheckComponentNull()
    {
        if (!TryGetComponent<CircleCollider2D>(out _circleCollider))
        {
            Debug.Log(transform.name + " " + "サークル無い");
            _isComponentNull = true;
        }
        if (_lineRenderer == null)
        {
            Debug.Log(transform.name + " " + "ライン無い");
            _isComponentNull = true;
        }
    }
}
