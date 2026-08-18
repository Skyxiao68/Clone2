using UnityEngine;

public class VisualKeepUpright : MonoBehaviour
{
    void LateUpdate()
    {
        
        transform.rotation = Quaternion.identity;
    }
}