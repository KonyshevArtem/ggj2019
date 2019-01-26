using UnityEngine;

public class InterfaceElementWorldPositioner : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;
    public Camera InterfaceCamera;

    void Update()
    {
        Vector3 targetScreenPosition = Camera.main.WorldToScreenPoint(Target.position);
        transform.position = InterfaceCamera.ScreenToWorldPoint(targetScreenPosition) + Offset;
    }
}