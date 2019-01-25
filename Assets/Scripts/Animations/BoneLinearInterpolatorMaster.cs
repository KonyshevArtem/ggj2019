using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoneLinearInterpolatorMaster : MonoBehaviour
{
    [Range(0, 1)]
    public float Weights;

    private List<BoneLinearInterpolator> linearInterpolators;

    void Start()
    {
        linearInterpolators = GetComponentsInChildren<BoneLinearInterpolator>().ToList();
    }

    void Update()
    {
        linearInterpolators.ForEach(interpolator => interpolator.Weight = Weights);
    }
}
