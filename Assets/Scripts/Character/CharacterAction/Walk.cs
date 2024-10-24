using UnityEngine;

public class Walk : MonoBehaviour, IWalk
{
    public void DoWalk(float inputX, float inputY)
    {
        transform.position += new Vector3(inputX, inputY, 0) * Time.deltaTime;
    }
}
