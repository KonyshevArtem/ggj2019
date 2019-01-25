using UnityEngine;

public class CameraPositionController : MonoBehaviour
{

    public Vector3 CameraOffset;

    void Update()
    {
        Camera.main.transform.position = transform.position + CameraOffset;
    }

}
