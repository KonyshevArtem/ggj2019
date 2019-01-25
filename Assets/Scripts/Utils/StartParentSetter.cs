using UnityEngine;

public class StartParentSetter : MonoBehaviour
{
    public Transform TargetParent;

    void Start()
    {
        transform.SetParent(TargetParent);
    }
}
