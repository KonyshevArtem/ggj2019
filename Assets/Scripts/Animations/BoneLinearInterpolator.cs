using UnityEngine;

public class BoneLinearInterpolator : MonoBehaviour
{

    public Transform RagdollBone, AnimationBone;
    [Range(0, 1)]
    public float Weight;

    void Update()
    {
        transform.rotation = Quaternion.Lerp(RagdollBone.rotation, AnimationBone.rotation, Weight);
    }
}
