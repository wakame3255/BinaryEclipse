using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class ObservationLogic : MonoBehaviour
{
    [SerializeField]
    StartStateNode _startStateNode;

    [SerializeField]
    List<BaseStateNode> _baseStateNodes = new List<BaseStateNode>();
    
    private void Update()
    {
       if( Input.GetKeyDown(KeyCode.K))
        {
            _baseStateNodes = _startStateNode.UpDateStateNodeFlow();
        }
    }

   private void ProgressStateNode()
    {

    }

}
